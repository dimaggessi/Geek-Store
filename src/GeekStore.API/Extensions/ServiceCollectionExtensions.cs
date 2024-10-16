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
    }
}