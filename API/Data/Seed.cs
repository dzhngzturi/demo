using API.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext dataContext)
        {
            if (await dataContext.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

           // var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@ssword1"));
                user.PasswordSalt = hmac.Key;

                dataContext.Users.Add(user);
            }

            await dataContext.SaveChangesAsync();
        }
    }
}
