using GeekStore.Domain.Interfaces;
using GeekStore.Infrastructure.Persistence;
using GeekStore.Infrastructure.Persistence.Repositories;
using GeekStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace GeekStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabaseContext(services, configuration);
            AddRedis(services, configuration);
            AddRepositories(services);
            AddInfraServices(services);
        }

        private static void AddDatabaseContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GeekStoreDbContext>(dbContextOptions => 
            {
                dbContextOptions.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
        }
        private static void AddRedis(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var connectionString = configuration.GetConnectionString("Redis") ??
                    throw new InvalidOperationException("Connection string was not found");

                var options = ConfigurationOptions.Parse(connectionString, true);
                return ConnectionMultiplexer.Connect(options);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();
        }

        private static void AddInfraServices(IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
        }
    }
}