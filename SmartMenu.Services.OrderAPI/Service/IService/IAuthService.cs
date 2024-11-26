
using SmartMenu.Services.OrderAPI.Models.Dto;

namespace SmartMenu.Services.OrderAPI.Service.IService
{
    public interface IAuthService
    {
        Task<int> GetStoreIdAsync(string userId);

       
    }
}
