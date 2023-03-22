using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.AddressLine).IsRequired().HasColumnType("ntext").HasMaxLength(250);
            builder.Property(I => I.City).IsRequired().HasMaxLength(30);
            builder.Property(I => I.District).IsRequired().HasMaxLength(30);
            builder.Property(I => I.PostalCode).IsRequired(false).HasMaxLength(10);
            builder.Property(I => I.Title).IsRequired().HasMaxLength(50);
            builder.Property(I => I.Neighborhood).IsRequired().HasMaxLength(100);
            
            builder.HasOne(I => I.AppUser).WithMany(I => I.Addresses).HasForeignKey(I => I.AppUserId);

            builder.HasMany(I => I.Orders).WithOne(I => I.Address).HasForeignKey(I => I.AddressId);
        }
    }
}
