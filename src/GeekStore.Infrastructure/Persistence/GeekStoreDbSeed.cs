using GeekStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json;

namespace GeekStore.Infrastructure.Persistence
{
    public static class GeekStoreDbSeed
    {
        public static async Task SeedAsync(GeekStoreDbContext context, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!userManager.Users.Any(u => u.UserName == "admin@test.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                };

                var adminPassword = config["Admin:Password"];

                if (string.IsNullOrEmpty(adminPassword))
                {
                    throw new InvalidOperationException("Admin password is required in appsettings.");
                }

                // Production: get from IConfiguration
                await userManager.CreateAsync(user, adminPassword);
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (!context.Products.Any())
            {
                var json = await File.ReadAllTextAsync(path + @"/Persistence/Data/products.json");

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
