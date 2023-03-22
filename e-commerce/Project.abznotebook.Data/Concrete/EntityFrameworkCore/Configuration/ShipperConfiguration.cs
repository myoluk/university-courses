using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Configuration
{
    public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.CompanyName).IsRequired();
            builder.Property(I => I.Phone).IsRequired();

            builder.HasMany(I => I.Orders).WithOne(I => I.Shipper).HasForeignKey(I => I.ShipperId);

        }
    }
}
