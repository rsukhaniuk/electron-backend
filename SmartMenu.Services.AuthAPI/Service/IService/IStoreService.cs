using SmartMenu.Services.AuthAPI.Models.Dto;

namespace SmartMenu.Services.AuthAPI.Service.IService
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetStores();
    }
}
