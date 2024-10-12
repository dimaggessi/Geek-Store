using System.Net;
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
                    return Results.Problem("Um erro inesperado aconteceu.");
                }

                return Results.Problem(new ProblemDetails 
                {
                    Title = "Um erro ocorreu ao processar sua requisição",
                    Detail = exception.Message,
                    Status = (int)HttpStatusCode.InternalServerError
                });
            });

            return app;
        }
    }

}

