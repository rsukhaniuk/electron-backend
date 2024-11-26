using SmartMenu.Services.OrderAPI.Models.Dto;
using SmartMenu.Services.OrderAPI.Service.IService;
using Newtonsoft.Json;

namespace SmartMenu.Services.OrderAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<int> GetStoreIdAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient("Auth");
            var response = await client.GetAsync($"/api/auth/GetStoreId/{userId}");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<int>(Convert.ToString(resp.Result));
            }
            return 0;
        }
    }
}
