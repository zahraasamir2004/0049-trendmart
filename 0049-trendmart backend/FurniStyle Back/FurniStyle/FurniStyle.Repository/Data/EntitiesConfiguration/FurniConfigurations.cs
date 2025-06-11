using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurniStyle.Core.Entities;

namespace FurniStyle.Repository.Data.EntitiesConfiguration
{
    public class FurniConfigurations : IEntityTypeConfiguration<Furni>
    {
        public void Configure(EntityTypeBuilder<Furni> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

            builder.Property(p => p.Description).HasMaxLength(1000);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(p => p.StockQuantity).IsRequired();

            builder.Property(p => p.PictureUrl).HasMaxLength(500);

            builder.HasOne(p => p.Category).WithMany()
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

             builder.HasOne(p => p.Room).WithMany()
                   .HasForeignKey(p => p.RoomId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
