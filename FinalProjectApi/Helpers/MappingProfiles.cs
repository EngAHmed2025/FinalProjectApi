using AutoMapper;
using FinalProject.Core.Models;
using FinalProject.Core.Order_Aggregrate;
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
            CreateMap<AddressDto , Address>();
            CreateMap<Order, OrderToReturnDto>()
            .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));
            CreateMap<OrderItems, OrderItemsDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                  .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                    .ForMember(d => d.ProductUrl, o => o.MapFrom(s => s.Product.ProductUrl))
                      .ForMember(d => d.ProductUrl, o => o.MapFrom<OrderItemsPictureUrlReslover>());
        }
    }
}
