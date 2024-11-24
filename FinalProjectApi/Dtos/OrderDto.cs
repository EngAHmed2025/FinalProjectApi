using FinalProject.Core.Order_Aggregrate;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectApi.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId{ get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; }
    }

  
}
