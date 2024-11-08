using AutoMapper;
using FinalProject.Core.Models;
using FinalProjectApi.Dtos;
using static System.Net.WebRequestMethods;

namespace FinalProjectApi.Helpers
{
    public class MappingProfiles : Profile
    {
       

        public MappingProfiles()
        {
           
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>(); 
        }
    }
}
