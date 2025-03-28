using GeekStore.Domain.Entities;
using GeekStore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
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

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    return true;
                });

                options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");

                options.AddSecurityDefinition("cookieAuth", new OpenApiSecurityScheme
                {
                    Name = "Cookie",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Cookie,
                    Scheme = "cookieAuth",
                    Description = "Autenticação baseada em cookies"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "cookieAuth"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddCorsWithOptions(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                //// Development Policy
                //options.AddPolicy("AllowAnyOrigin",
                //    builder =>
                //    {
                //        builder.AllowAnyOrigin()
                //               .AllowAnyMethod()
                //               .AllowAnyHeader();
                //    });

                // Production Policy
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins(
                                "https://www.dimaggessi.info",
                                "http://www.dimaggessi.info",
                                "https://dimaggessi.info",
                                "http://dimaggessi.info",
                                "http://nginx-angular-app",    // frontend container
                                "http://localhost:4200",       // local development
                                "https://localhost",           // local development
                                "http://localhost:80",
                                "https://localhost:443")
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials() // cookies, tokens, etc.
                               .SetIsOriginAllowed((host) => true); // websocket
                    });
            });

            return services;
        }
    }
}