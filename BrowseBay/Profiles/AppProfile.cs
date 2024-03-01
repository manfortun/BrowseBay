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
            CreateMap<ProductOnPurchaseDto, Product>();
            CreateMap<ProductReadDto, Product>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductOnPurchaseDto>();
            CreateMap<Product, ProductReadDto>();
            CreateMap<Product, ProductCreateDto>();
            CreateMap<Product, ProductDto>();

            // Categories
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryDto, Category>();

            // Product Categories
            CreateMap<ProductCategoryDto, ProductCategory>();

            // Purchases
            CreateMap<PurchaseReadDto, Purchase>();
            CreateMap<Purchase, PurchaseReadDto>();
        }
    }
}
