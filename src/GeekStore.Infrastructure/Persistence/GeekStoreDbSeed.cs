using GeekStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace GeekStore.Infrastructure.Persistence
{
    public static class GeekStoreDbSeed
    {
        public static async Task SeedAsync(GeekStoreDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any(u => u.UserName == "admin@test.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                };

                // Production: get from IConfiguration
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (!context.Products.Any())
            {
                var json = await File.ReadAllTextAsync("../GeekStore.Infrastructure/Persistence/Data/products.json");

                // ignore case sensitivity
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var products = JsonSerializer.Deserialize<List<Product>>(json, options);

                if (products is null)
                    return;

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
