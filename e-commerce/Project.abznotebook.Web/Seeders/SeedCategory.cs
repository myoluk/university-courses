using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Project.abznotebook.Data.Concrete.EntityFrameworkCore.Contexts;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Web
{
    public static class SeedCategory
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            TechnoStoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<TechnoStoreDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    
                    new Category()
                    {
                        Name = "Gaming Laptop",
                        Description = "Yüksek miktarda işlem gücü gerektiren video oyunlarını oynamak için tasarlanmış kişisel bilgisayarlar.",
                    },

                    new Category()
                    {
                        Name = "Ev - Ofis Laptop'ları",
                        Description = "Günlük temel düzeyde kullanımı temel alan ev - ofis bilgisayarları.",
                    },

                    new Category()
                    {
                        Name = "İkisi Bir Arada",
                        Description = "Hem normal hem de dokunmatik işleve sahip bilgisayarlar"
                    }

                );
                context.SaveChanges();
            }

        }
    }
}