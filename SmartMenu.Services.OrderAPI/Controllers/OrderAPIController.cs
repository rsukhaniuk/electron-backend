using AutoMapper;
using SmartMenu.Services.OrderAPI.Data;
using SmartMenu.Services.OrderAPI.Models;
using SmartMenu.Services.OrderAPI.Models.Dto;
using SmartMenu.Services.OrderAPI.Utility;
using SmartMenu.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using Microsoft.EntityFrameworkCore;

namespace SmartMenu.Services.OrderAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private IProductService _productService;
        private readonly IConfiguration _configuration;
        public OrderAPIController(AppDbContext db,
            IProductService productService, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            this._response = new ResponseDto();
            _productService = productService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("GetOrders")]
        public ResponseDto? Get(string? userId = "")
        {
            try
            {
                IEnumerable<OrderHeader> objList;
                if (User.IsInRole(SD.RoleAdmin))
                {
                    objList = _db.OrderHeaders.Include(u => u.OrderDetails).OrderByDescending(u => u.OrderHeaderId).ToList();
                }
                else
                {
                    objList = _db.OrderHeaders.Include(u => u.OrderDetails).Where(u=>u.UserId==userId).OrderByDescending(u => u.OrderHeaderId).ToList();
                }
                _response.Result = _mapper.Map<IEnumerable<OrderHeaderDto>>(objList);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Authorize]
        [HttpGet("GetOrder/{id:int}")]
        public ResponseDto? Get(int id)
        {
            try
            {
                OrderHeader orderHeader = _db.OrderHeaders.Include(u => u.OrderDetails).First(u => u.OrderHeaderId == id);
                _response.Result = _mapper.Map<OrderHeaderDto>(orderHeader);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }



        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder([FromBody] CartDto cartDto, [FromQuery] string deliveryMethod, [FromQuery] string? paymentMethod = null)
        {
            try
            {
                bool isManager = User.IsInRole(SD.RoleManager);

                if (isManager)
                {
                    paymentMethod = SD.PaymentMethod_OnPickup;
                }
                else
                {
                    if (string.IsNullOrEmpty(paymentMethod))
                    {
                        throw new Exception("Payment method is required for customer orders.");
                    }

                    if (deliveryMethod == SD.DeliveryMethod_Courier && paymentMethod != SD.PaymentMethod_Card)
                    {
                        throw new Exception("Courier delivery requires payment by card.");
                    }

                    if (deliveryMethod == SD.DeliveryMethod_SelfPickup &&
                        paymentMethod != SD.PaymentMethod_Card &&
                        paymentMethod != SD.PaymentMethod_OnPickup)
                    {
                        throw new Exception("Invalid payment method for self-pickup.");
                    }
                }

                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
                orderHeaderDto.OrderTime = DateTime.Now;
                orderHeaderDto.Status = SD.Status_Pending;
                orderHeaderDto.DeliveryMethod = deliveryMethod;
                orderHeaderDto.PaymentMethod = paymentMethod!;
                orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);
                orderHeaderDto.OrderTotal = Math.Round(orderHeaderDto.OrderTotal, 2);

                OrderHeader orderCreated = _db.OrderHeaders.Add(_mapper.Map<OrderHeader>(orderHeaderDto)).Entity;
                await _db.SaveChangesAsync();

                orderHeaderDto.OrderHeaderId = orderCreated.OrderHeaderId;
                _response.Result = orderHeaderDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [Authorize]
        [HttpPost("CreateStripeSession")]
        public async Task<ResponseDto> CreateStripeSession([FromBody] StripeRequestDto stripeRequestDto)
        {
            try
            {
                
                var options = new SessionCreateOptions
                {
                    SuccessUrl = stripeRequestDto.ApprovedUrl,
                    CancelUrl = stripeRequestDto.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),                     
                    Mode = "payment",
                    
                };

                

                foreach (var item in stripeRequestDto.OrderHeader.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100), // $20.99 -> 2099
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            }
                        },
                        Quantity = item.Count
                    };

                    options.LineItems.Add(sessionLineItem);
                }

                
                var service = new SessionService();
                Session session = service.Create(options);
                stripeRequestDto.StripeSessionUrl = session.Url;
                OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == stripeRequestDto.OrderHeader.OrderHeaderId);
                orderHeader.StripeSessionId = session.Id;
                _db.SaveChanges();
                _response.Result = stripeRequestDto;

            }
            catch(Exception ex)
            {
                _response.Message= ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }


        [Authorize]
        [HttpPost("ValidateStripeSession")]
        public async Task<ResponseDto> ValidateStripeSession([FromBody] int orderHeaderId)
        {
            try
            {

                OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == orderHeaderId);

                var service = new SessionService();
                Session session = service.Get(orderHeader.StripeSessionId);

                var paymentIntentService = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

                if(paymentIntent.Status== "succeeded")
                {
                    //then payment was successful
                    orderHeader.PaymentIntentId = paymentIntent.Id;
                    orderHeader.Status = SD.Status_Payed; 
                    _db.SaveChanges();
                    _response.Result = _mapper.Map<OrderHeaderDto>(orderHeader);
                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }


        [Authorize]
        [HttpPost("UpdateOrderStatus/{orderId:int}")]
        public async Task<ResponseDto> UpdateOrderStatus(int orderId, [FromBody] string newStatus)
        {
            try
            {
                var orderHeader = _db.OrderHeaders.FirstOrDefault(u => u.OrderHeaderId == orderId);
                if (orderHeader == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Order not found.";
                    return _response;
                }

                if (newStatus == SD.Status_Cancelled)
                {
                    if (orderHeader.DeliveryMethod == SD.DeliveryMethod_Courier && orderHeader.Status == SD.Status_InDelivery)
                    {
                        _response.IsSuccess = false;
                        _response.Message = "Order cannot be cancelled as it is already in delivery.";
                        return _response;
                    }

                    if (orderHeader.DeliveryMethod == SD.DeliveryMethod_SelfPickup && orderHeader.Status == SD.Status_Delivered)
                    {
                        _response.IsSuccess = false;
                        _response.Message = "Order cannot be cancelled as it is already ready for pickup.";
                        return _response;
                    }

                    if (!string.IsNullOrEmpty(orderHeader.PaymentIntentId) && orderHeader.PaymentMethod == SD.PaymentMethod_Card)
                    {
                        var refundOptions = new RefundCreateOptions
                        {
                            Reason = RefundReasons.RequestedByCustomer,
                            PaymentIntent = orderHeader.PaymentIntentId
                        };
                        var refundService = new RefundService();
                        Refund refund = refundService.Create(refundOptions);
                    }
                    else if (orderHeader.PaymentMethod == SD.PaymentMethod_OnPickup && orderHeader.Status != SD.Status_Payed)
                    {
                        _response.Message = "Order cancelled. No refund needed as it was not paid.";
                    }
                }

                orderHeader.Status = newStatus;
                await _db.SaveChangesAsync();

                _response.Message = $"Order status updated to {newStatus}.";
                _response.Result = _mapper.Map<OrderHeaderDto>(orderHeader);
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
