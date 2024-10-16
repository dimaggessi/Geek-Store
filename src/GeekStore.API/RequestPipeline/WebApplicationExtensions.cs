using System.Net;
using GeekStore.Domain.Shared;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
    }

}

