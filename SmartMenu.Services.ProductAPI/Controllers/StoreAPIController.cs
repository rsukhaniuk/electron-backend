using AutoMapper;
using SmartMenu.Services.ProductAPI.Data;
using SmartMenu.Services.ProductAPI.Models;
using SmartMenu.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartMenu.Services.ProductAPI.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public StoreAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        // GET: api/store
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Store> storeList = _db.Stores.ToList();
                _response.Result = _mapper.Map<IEnumerable<StoreDto>>(storeList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // GET: api/store/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Store store = _db.Stores.FirstOrDefault(s => s.StoreId == id);
                if (store == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Store not found";
                    return _response;
                }

                _response.Result = _mapper.Map<StoreDto>(store);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // POST: api/store
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post(StoreDto storeDto)
        {
            try
            {
                Store store = _mapper.Map<Store>(storeDto);
                _db.Stores.Add(store);
                _db.SaveChanges();

                _response.Result = _mapper.Map<StoreDto>(store);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // PUT: api/store
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put(StoreDto storeDto)
        {
            try
            {
                Store store = _mapper.Map<Store>(storeDto);

                if (!_db.Stores.Any(s => s.StoreId == store.StoreId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Store not found";
                    return _response;
                }

                _db.Stores.Update(store);
                _db.SaveChanges();

                _response.Result = _mapper.Map<StoreDto>(store);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // DELETE: api/store/{id}
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Store store = _db.Stores.FirstOrDefault(s => s.StoreId == id);
                if (store == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Store not found";
                    return _response;
                }

                _db.Stores.Remove(store);
                _db.SaveChanges();

                _response.Result = true;
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
