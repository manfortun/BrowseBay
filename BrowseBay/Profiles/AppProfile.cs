using AutoMapper;
using BrowseBay.Models;
using BrowseBay.Service.DTOs;

namespace BrowseBay.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            // Login
            CreateMap<SignUpDto, LogInDto>();

            // Products
            CreateMap<ProductReadDto, Product>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductReadDto>();
            CreateMap<Product, ProductDto>();

            // Categories
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryDto, Category>();

            // Product Categories
            CreateMap<ProductCategoryDto, ProductCategory>();
        }
    }
}
