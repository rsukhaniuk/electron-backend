﻿
namespace SmartMenu.Services.OrderAPI.Models.Dto
{
    public class OrderHeaderDto
    {
        public int OrderHeaderId { get; set; }
        public string? UserId { get; set; }
        public double OrderTotal { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime OrderTime { get; set; }
        public string? Status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }
        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
        public string DeliveryMethod { get; set; }
        public string PaymentMethod { get; set; }
        public int? StoreId { get; set; }

        public string? ShippingAddress { get; set; }
    }
}
