
using SmartMenu.Services.OrderAPI.Models.Dto;

namespace SmartMenu.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<CategoryDto> GetCategoryById(int CategoryId);
    }
}
