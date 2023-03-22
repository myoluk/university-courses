using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(I => I.Name).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Surname).HasMaxLength(100).IsRequired();


            builder.HasMany(I => I.Orders).WithOne(I => I.Customer).HasForeignKey(I => I.CustomerId).OnDelete(deleteBehavior: DeleteBehavior.NoAction);
        }
    }
}
