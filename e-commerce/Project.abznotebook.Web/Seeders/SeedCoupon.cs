using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.abznotebook.Data.Concrete.EntityFrameworkCore.Contexts;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Web.Seeders
{
    public class SeedCoupon
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            TechnoStoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<TechnoStoreDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Coupons.Any())
            {
                context.Coupons.AddRange(

                    new Coupon()
                    {
                        Code = "COUPONFIRST",
                        EndDate = DateTime.Now,
                        DiscountPercentage = 30,
                        IsActive = true
                    },

                    new Coupon()
                    {
                        Code = "COUPONSECOND",
                        EndDate = new DateTime(2020,12,25),
                        DiscountPercentage = 30,
                        IsActive = true
                    }

                );
                context.SaveChanges();
            }

        }
    }
}
