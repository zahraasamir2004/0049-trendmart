using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurniStyle.Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Data.EntitiesConfiguration
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(p => p.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Status).HasConversion(o=>o.ToString(),o=> (OrderStatus)Enum.Parse(typeof(OrderStatus),o));
            builder.OwnsOne(p=>p.ShippingAddress,o=>o.WithOwner());
            builder.HasOne(o=>o.DelivaryMethod).WithMany().HasForeignKey(o=>o.DelivaryMethodId);

        }
    }
}
