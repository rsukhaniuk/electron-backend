using SmartMenu.Services.OrderAPI.Models.Dto;
using SmartMenu.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace SmartMenu.Services.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
            }
            return new List<ProductDto>();
        }

        public async Task<CategoryDto> GetCategoryById(int CategoryId)
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/category/{CategoryId}");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(resp.Result));
            }
            return new CategoryDto();
        }
    }
}
