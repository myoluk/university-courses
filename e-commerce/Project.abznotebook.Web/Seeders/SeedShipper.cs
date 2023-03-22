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
    public static class SeedShipper
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            TechnoStoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<TechnoStoreDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Shippers.Any())
            {
                context.Shippers.AddRange(

                    new Shipper()
                    {
                        CompanyName = "Yurtiçi Kargo",
                        Phone = "444 99 99"
                    },

                    new Shipper()
                    {
                        CompanyName = "Aras Kargo",
                        Phone = "444 25 52"

                    },

                    new Shipper()
                    {
                        CompanyName = "MNG Kargo",
                        Phone = "444 06 06"
                    },

                    new Shipper()
                    {
                        CompanyName = "Sürat Kargo",
                        Phone = "0850 202 0 202"
                    },

                    new Shipper()
                    {
                        CompanyName = "PTT Kargo",
                        Phone = "444 17 88"
                    },

                    new Shipper()
                    {
                        CompanyName = "UPS",
                        Phone = "444 00 33"
                    }

                );
                context.SaveChanges();
            }

        }
    }
}
