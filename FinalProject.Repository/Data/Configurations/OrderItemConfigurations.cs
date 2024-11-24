using FinalProject.Core.Order_Aggregrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Repository.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.OwnsOne(orderItem => orderItem.Product, product => product.WithOwner());
            builder.Property(Item => Item.Price)
                .HasColumnType("decimal(18,2)");

           
        }
    }
}
