using System.Net;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using GeekStore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.API.RequestPipeline
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            // define como as exceções serão tratadas em nível de aplicativo
            // redireciona para o endpoit "/error"
            app.UseExceptionHandler("/error");

            app.Map("/error", (HttpContext httpContext) =>
            {
                var exceptionFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
                Exception? exception = exceptionFeature?.Error;

                if (exception is null)
                {
                    return Results.Problem(ResourceErrorMessages.UNEXPECTED_ERROR);
                }

                return Results.Problem(new ProblemDetails 
                {
                    Title = ResourceErrorMessages.REQUEST_ERROR,
                    Detail = exception.Message,
                    Status = (int)HttpStatusCode.InternalServerError
                });
            });

            return app;
        }

        public static async Task PopulateDatabase(this WebApplication app)
        {
            try
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<GeekStoreDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var config = services.GetRequiredService<IConfiguration>();
                await context.Database.MigrateAsync();

                await GeekStoreDbSeed.SeedAsync(context, userManager, config);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}

