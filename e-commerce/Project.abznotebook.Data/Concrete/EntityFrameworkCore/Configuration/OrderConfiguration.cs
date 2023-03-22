using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(I => I.Id);

            builder.HasMany(I => I.OrderDetails).WithOne(I => I.Order).HasForeignKey(I => I.OrderId);
        }
    }
}
