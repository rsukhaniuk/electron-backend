using AutoMapper;
using SmartMenu.Services.ProductAPI.Models;
using SmartMenu.Services.ProductAPI.Models.Dto;

namespace SmartMenu.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<CategoryDto, Category>().ReverseMap();
                config.CreateMap<StoreDto, Store>().ReverseMap();
                config.CreateMap<ProductStoreDto, ProductStore>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
