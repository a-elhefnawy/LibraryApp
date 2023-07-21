using LibraryApp.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Seeded_Data
{
    public static class DefaultUser
    {
        public static async Task SeedUser(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new AppUser()
            {
                Name = "Admin Admin",
                Email = "Admin@gmail.com",
                UserName = "Admin007"
            };
            if(!roleManager.Roles.Any()) 
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var checktable=  userManager.Users.Any();
            if (!checktable)
            {
                await userManager.CreateAsync(defaultUser,"As@123456");
                await userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
