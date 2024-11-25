using AutoMapper;
using SmartMenu.Services.ProductAPI.Data;
using SmartMenu.Services.ProductAPI.Models;
using SmartMenu.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SmartMenu.Services.ProductAPI.Controllers
{
    [Route("api/productstore")]
    [ApiController]
    public class ProductStoreAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public ProductStoreAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        // Get all ProductStore entries
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                var productStores = _db.ProductStores
                    .Include(ps => ps.Store) // Include Store details
                    .Include(ps => ps.Product) // Include Product details
                    .ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductStoreDto>>(productStores);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // Get a specific ProductStore by ID
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                var productStore = _db.ProductStores
                    .Include(ps => ps.Store) // Include Store details
                    .Include(ps => ps.Product) // Include Product details
                    .FirstOrDefault(ps => ps.ProductStoreId == id);
                _response.Result = _mapper.Map<ProductStoreDto>(productStore);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // Add a new ProductStore entry
        [HttpPost]
        [Authorize(Roles = "ADMIN,MANAGER")]
        public ResponseDto Post(ProductStoreDto productStoreDto)
        {
            try
            {
                var productStore = _mapper.Map<ProductStore>(productStoreDto);

                if (!_db.Products.Any(p => p.ProductId == productStore.ProductId) ||
                    !_db.Stores.Any(s => s.StoreId == productStore.StoreId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid Product or Store ID.";
                    return _response;
                }

                _db.ProductStores.Add(productStore);
                _db.SaveChanges();
                _response.Result = _mapper.Map<ProductStoreDto>(productStore);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // Update an existing ProductStore entry
        [HttpPut]
        [Authorize(Roles = "ADMIN,MANAGER")]
        public ResponseDto Put(ProductStoreDto productStoreDto)
        {
            try
            {
                var productStore = _mapper.Map<ProductStore>(productStoreDto);

                if (!_db.Products.Any(p => p.ProductId == productStore.ProductId) ||
                    !_db.Stores.Any(s => s.StoreId == productStore.StoreId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid Product or Store ID.";
                    return _response;
                }

                _db.ProductStores.Update(productStore);
                _db.SaveChanges();
                _response.Result = _mapper.Map<ProductStoreDto>(productStore);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // Delete a ProductStore entry
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN,MANAGER")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var productStore = _db.ProductStores
                    .Include(ps => ps.Store) // Include Store details
                    .Include(ps => ps.Product) // Include Product details
                    .FirstOrDefault(ps => ps.ProductStoreId == id);

                if (productStore == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "ProductStore not found.";
                    return _response;
                }

                _db.ProductStores.Remove(productStore);
                _db.SaveChanges();
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
