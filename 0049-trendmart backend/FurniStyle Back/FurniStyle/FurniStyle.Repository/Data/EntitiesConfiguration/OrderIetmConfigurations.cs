using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurniStyle.Core.Entities;
using FurniStyle.Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Data.EntitiesConfiguration
{
    public class OrderIetmConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(p => p.productItemOrder, p => p.WithOwner());
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
