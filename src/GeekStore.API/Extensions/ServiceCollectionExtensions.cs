namespace GeekStore.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
        {
            // Fornece informações personalizadas
            services.AddProblemDetails(options => 
            {
                options.CustomizeProblemDetails = context =>
                {
                    // adiciona a propriedade customizada traceId
                    context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                };
            });

            return services;
        }
    }
}