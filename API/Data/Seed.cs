﻿using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

           // var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);

            var roles = new List<AppRole>
            {
                new AppRole {   Name = "Admin" },
                new AppRole {   Name = "Member" },
                new AppRole {   Name = "Moderator" },
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.Photos.First().isApproved = true;
                user.UserName = user.UserName.ToLower();

                await userManager.CreateAsync(user, "P@ssword1");

                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "Admin"
            };

            await userManager.CreateAsync(admin, "P@ssword1");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
        }
    }
}
