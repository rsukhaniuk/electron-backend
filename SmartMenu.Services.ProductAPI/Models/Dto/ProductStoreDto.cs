using SmartMenu.Services.ProductAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace SmartMenu.Services.ProductAPI.Models
{
    public class ProductStoreDto
    {
        public int ProductStoreId { get; set; }
        public int StoreId { get; set; }
        public StoreDto? Store { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public bool IsAvailable { get; set; }
    }
}
