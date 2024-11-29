using AutoMapper;
using FinalProject.Core.Order_Aggregrate;
using FinalProjectApi.Dtos;

namespace FinalProjectApi.Helpers
{
    public class OrderItemsPictureUrlReslover : IValueResolver<OrderItems, OrderItemsDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemsPictureUrlReslover(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public string Resolve(OrderItems source, OrderItemsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.ProductUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.Product.ProductUrl}";
            }

            return string.Empty;
        }
    }
}
