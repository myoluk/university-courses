using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Data.Concrete.EntityFrameworkCore.Configuration
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(I => I.Id);

            builder.HasIndex(I => I.Code).IsUnique(true);
            builder.Property(I => I.EndDate).IsRequired().HasColumnType("smalldatetime");
            builder.Property(I => I.IsActive).IsRequired();
            builder.Property(I => I.DiscountPercentage).IsRequired();
        }
    }
}