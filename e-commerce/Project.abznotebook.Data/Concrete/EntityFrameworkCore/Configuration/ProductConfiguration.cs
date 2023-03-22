using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Name).HasMaxLength(250).IsRequired();
            builder.Property(I => I.Description).HasColumnType("ntext");
            builder.Property(I => I.DiscCapacity).IsRequired(false);
            builder.Property(I => I.ProcessorType).IsRequired(false);
            builder.Property(I => I.GraphicsCard).IsRequired(false);
            builder.Property(I => I.ProcessorVendor).IsRequired(false);
            builder.Property(I => I.MemoryCapacity).IsRequired(false);
            builder.Property(I => I.UnitInStock).IsRequired();
            builder.Property(I => I.Vendor).IsRequired();
            builder.Property(I => I.SKU).IsRequired();
            builder.Property(I => I.Image1).IsRequired();
            builder.Property(I => I.Image2).IsRequired(false);
            builder.Property(I => I.Image3).IsRequired(false);
            builder.Property(I => I.UnitPrice).IsRequired();

            builder.HasMany(I => I.OrderDetails).WithOne(I => I.Product).HasForeignKey(I => I.ProductId);
        }
    }
}
