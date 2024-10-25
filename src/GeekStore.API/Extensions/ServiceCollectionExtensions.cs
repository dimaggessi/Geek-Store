using GeekStore.Domain.Entities;
using GeekStore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace GeekStore.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
        {
            // Detailed information
            services.AddProblemDetails(options => 
            {
                options.CustomizeProblemDetails = context =>
                {
                    // add traceId property
                    context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                };
            });

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<GeekStoreDbContext>();

            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GeekStore API",
                    Version = "v1"
                });

                // Adiciona tags manualmente aos controladores, se necessário
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    return true; // Incluir todas as rotas no mesmo documento (v1)
                });

                // Adicionar descrições nas tags, como "Authorization" para as rotas de autenticação
                options.TagActionsBy(apiDesc =>
                {
                    if (apiDesc.RelativePath?.StartsWith("api/auth") == true)
                    {
                        return new[] { "Authorization" }; // Atribuir tag "Authorization" aos endpoints de autenticação
                    }

                    return new[] { "GeekStore API" }; // Atribuir tag "GeekStore API" aos demais endpoints
                });

                options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
            });

            return services;
        }
    }
}