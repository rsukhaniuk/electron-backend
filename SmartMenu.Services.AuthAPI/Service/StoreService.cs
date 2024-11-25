using Newtonsoft.Json;
using SmartMenu.Services.AuthAPI.Models.Dto;
using SmartMenu.Services.AuthAPI.Service.IService;

namespace SmartMenu.Services.AuthAPI.Service
{
    public class StoreService : IStoreService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StoreService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<IEnumerable<StoreDto>> GetStores()
        {
            var client = _httpClientFactory.CreateClient("Store");
            var response = await client.GetAsync($"/api/store");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<StoreDto>>(Convert.ToString(resp.Result));
            }
            return new List<StoreDto>();
        }
    }
}
