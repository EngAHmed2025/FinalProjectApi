using FinalProject.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Order_Aggregrate
{
    public class Order : BaseEntity
    {
  
        public Order(string buyerEmail, Address shippingAddress, int deliveryMethodId, DeliveryMethod deliveryMethod, ICollection<OrderItems> orderitems, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethodId = deliveryMethodId;
            DeliveryMethod = deliveryMethod;
            Items = orderitems;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }

        public Address ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }

        public ICollection<OrderItems> Items { get; set; } = new HashSet<OrderItems>();

        public decimal SubTotal { get; set; }
        [NotMapped]
        public decimal Total { get; set; }
        public decimal GetTotal()
          => SubTotal + DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; }
    }
}
