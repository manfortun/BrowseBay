using AutoMapper;
using BrowseBay.Models;
using BrowseBay.Service.DTOs;

namespace BrowseBay.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CredentialsCreateDto, LogInDto>();
            CreateMap<Product, ProductReadDto>();
        }
    }
}
