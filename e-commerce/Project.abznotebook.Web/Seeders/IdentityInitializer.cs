using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Web
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");

            if (adminRole == null)
                await roleManager.CreateAsync(new AppRole() { Name = "Admin" });

            var memberRole = await roleManager.FindByNameAsync("member");

            if (memberRole == null)
                await roleManager.CreateAsync(new AppRole { Name = "Member" });

            var adminUser = await userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                AppUser user = new AppUser()
                {
                    Name = "İsmet",
                    Surname = "Konuç",
                    Email = "ismetkonuc@gmail.com",
                    UserName = "admin"
                };

                await userManager.CreateAsync(user, "1");
                await userManager.AddToRoleAsync(user, "Admin");
            }

        }
    }
}
