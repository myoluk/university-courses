using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Name).IsRequired();
            builder.Property(I => I.Description).HasColumnType("ntext");
            builder.Property(I => I.Description).HasColumnName("Description");

            builder.HasMany(I => I.Products).WithOne(I => I.Category).HasForeignKey(I => I.CategoryId)
                .OnDelete(deleteBehavior: DeleteBehavior.SetNull);
        }
    }
}
