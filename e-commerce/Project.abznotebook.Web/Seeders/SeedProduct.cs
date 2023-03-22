using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Project.abznotebook.Data.Concrete.EntityFrameworkCore.Contexts;
using Project.abznotebook.Entities.Concrete;
using Project.abznotebook.Data.Interfaces;

namespace Project.abznotebook.Web
{
    public static class SeedProduct
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            TechnoStoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<TechnoStoreDbContext>();
            if (EnumerableExtensions.Any(context.Database.GetPendingMigrations()))
            {
                context.Database.Migrate();
            }

            if (!EnumerableExtensions.Any(context.Products))
            {
                context.Products.AddRange(
                    
                    new Product()
                    {
                        Name = "Asus X515JF-BR070T",
                        Vendor = "Asus",
                        RealPrice = 12200,
                        UnitPrice = 4898,
                        UnitInStock = 10,
                        CategoryId = 2,
                        SKU = "TSV00000X5TSR",
                        ProcessorVendor = "Intel",
                        ProcessorType = "Intel Core i3 1005G1",
                        GraphicsCard = "Intel UHD Graphics",
                        DiscCapacity = "256 GB",
                        MemoryCapacity = "4 GB",
                        Description = "Asus X515JF-BR070T Intel Core i3 1005G1 4GB 256GB SSD Windows 10 Home 15.6 Taşınabilir Bilgisayar",
                        Image1 = "format_webp (1).jpg",
                        Image2 = "format_webp (3).jpg",
                        Image3 = "format_webp (4).jpg"
                    },

                    new Product()
                    {
                        Name = "MSI GF63 Thin 9SCSR-1053XTR",
                        Vendor = "MSI",
                        RealPrice = 14751,
                        UnitPrice = 7776,
                        UnitInStock = 25,
                        CategoryId = 1,
                        SKU = "TSV00000X9GRY",
                        ProcessorVendor = "Intel",
                        ProcessorType = "Intel Core i5 9300H",
                        GraphicsCard = "NVIDIA GTX 1650Ti",
                        DiscCapacity = "256 GB",
                        MemoryCapacity = "8 GB",
                        Description = "MSI GF63 Thin 9SCSR-1053XTR Intel Core i5 9300H 8GB 256GB SSD GTX 1650Ti Freedos 15.6\" FHD Taşınabilir Bilgisayar",
                        Image1 = "msigf63_1.jpg",
                        Image2 = "msigf63_2.jpg",
                        Image3 = "msigf63_3.jpg"
                    },

                    new Product()
                    {
                        Name = "Dell Vostro 3501",
                        Vendor = "Dell",
                        RealPrice = 6250,
                        UnitPrice = (decimal) (5999.4),
                        UnitInStock = 100,
                        CategoryId = 2,
                        SKU = "TSV00000X9GRQ",
                        ProcessorVendor = "Intel",
                        ProcessorType = "Intel Core i3 1005G1",
                        GraphicsCard = "Intel UHD Graphics",
                        DiscCapacity = "1 TB",
                        MemoryCapacity = "8 GB",
                        Description = "Dell Vostro 3501 Intel Core i3 1005G1 8GB 256GB SSD Windows 10 Home 15.6\" FHD Taşınabilir Bilgisayar FB05W82N",
                        Image1 = "Dell Vostro 3501_1.jpg",
                        Image2 = "Dell Vostro 3501_2.jpg",
                        Image3 = "Dell Vostro 3501_3.jpg"
                    },

                    new Product()
                    {
                        Name = "Apple Macbook Pro M1",
                        Vendor = "Apple",
                        RealPrice = 20100,
                        UnitPrice = (decimal)(14298.99),
                        UnitInStock = 3,
                        CategoryId = 2,
                        SKU = "TSV00000OSBNX",
                        ProcessorVendor = "Apple",
                        ProcessorType = "M1",
                        GraphicsCard = "AMD Radeon Pro 5600M",
                        DiscCapacity = "256 GB",
                        MemoryCapacity = "8 GB",
                        Description = "Apple Macbook Pro M1 Çip 8GB 256GB macOS 13\" QHD Taşınabilir Bilgisayar Uzay Grisi MYD82TU/A",
                        Image1 = "Apple Macbook Pro M1_1.jpg",
                        Image2 = "Apple Macbook Pro M1_2.jpg",
                        Image3 = "Apple Macbook Pro M1_3.jpg"
                    },

                    new Product()
                    {
                        Name = "Lenovo IdeaPad Creator 5",
                        Vendor = "Lenovo",
                        RealPrice = 10000,
                        UnitPrice = 9699,
                        UnitInStock = 30,
                        CategoryId = 1,
                        SKU = "TSV00000LK9W1",
                        ProcessorVendor = "Intel",
                        ProcessorType = "Intel Core i5 10300H",
                        GraphicsCard = "NVIDIA GTX 1650Ti",
                        DiscCapacity = "512 GB",
                        MemoryCapacity = "16 GB",
                        Description = "Lenovo IdeaPad Creator 5 Intel Core i5 10300H 16GB 512GB SSD GTX 1650Ti Freedos 15.6'\' FHD Taşınabilir Bilgisayar 82D4002KTX",
                        Image1 = "Lenovo IdeaPad Creator 5_1.jpg",
                        Image2 = "Lenovo IdeaPad Creator 5_2.jpg",
                        Image3 = "Lenovo IdeaPad Creator 5_3.jpg"
                    },

                    new Product()
                    {
                        Name = "Asus TUF Gaming A15 FA506II-BQ200",
                        Vendor = "Asus",
                        RealPrice = 12200,
                        UnitPrice = 10114,
                        UnitInStock = 60,
                        CategoryId = 1,
                        SKU = "TSV00000UMKL4",
                        ProcessorVendor = "AMD",
                        ProcessorType = "AMD Ryzen 7 4800H",
                        GraphicsCard = "NVIDIA GTX1650Ti",
                        DiscCapacity = "1 TB",
                        MemoryCapacity = "8 GB",
                        Description = "Asus TUF Gaming A15 FA506II-BQ200 AMD Ryzen 7 4800H 8GB 1TB + 256GB SSD GTX1650Ti FreeDos 15.6\" FHD Taşınabilir Bilgisayar",
                        Image1 = "Asus TUF Gaming A15 FA506II-BQ200_1.jpg",
                        Image2 = "Asus TUF Gaming A15 FA506II-BQ200_2.jpg",
                        Image3 = "Asus TUF Gaming A15 FA506II-BQ200_3.jpg"
                    }

                    );
                context.SaveChanges();
            }
        }
    }
}