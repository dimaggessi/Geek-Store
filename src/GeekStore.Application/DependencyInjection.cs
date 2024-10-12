using System.Reflection;
using FluentValidation;
using GeekStore.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => 
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}