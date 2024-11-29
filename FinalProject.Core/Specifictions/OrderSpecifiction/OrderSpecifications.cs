using FinalProject.Core.Order_Aggregrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Specifictions.OrderSpecifiction
{
    public class OrderSpecifications : BaseSpecifictions<Order>
    {
        public OrderSpecifications(string buyerEmail) :base(o=>o.BuyerEmail == buyerEmail )
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o=>o.Items);

            AddOrderBy(o=>o.OrderDate);
          
        }

        public OrderSpecifications(int orderId , string buyerEmail)
            :base(o=>o.Id == orderId && o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

        }
    }
}
