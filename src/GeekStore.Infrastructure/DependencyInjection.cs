using GeekStore.Domain.Interfaces;
using GeekStore.Infrastructure.Persistence;
using GeekStore.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabaseContext(services, configuration);
            AddRepositories(services);
        }

        private static void AddDatabaseContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GeekStoreDbContext>(dbContextOptions => 
            {
                dbContextOptions.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }
        
    }
}