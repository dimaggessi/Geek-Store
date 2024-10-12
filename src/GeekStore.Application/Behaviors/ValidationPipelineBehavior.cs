using FluentValidation;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationPipelineBehavior(IValidator<TRequest>? validator) =>
            _validator = validator;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {

            if (_validator is null)
            {
                return await next();
            }

            var result = await _validator.ValidateAsync(request, cancellationToken);

            if (result.IsValid)
            {
                return await next();
            }


            Error[] errors = result.Errors.Select(failure => 
                new Error(failure.PropertyName, failure.ErrorMessage))
                .Distinct()
                .ToArray();

            if (errors.Any())
            {
                return CreateValidationResult<TResponse>(errors);
            }

            return await next();
        }

        private static TResult CreateValidationResult<TResult>(Error[] errors) 
            where TResult : Result
        {
            
            var isTypeof = typeof(TResult) == typeof(Result);

            if (isTypeof)
            {
                return (ValidationResult.WithErrors(errors) as TResult)!;
            }

            // When TResult is a Generic Result Object
            object validationResult = typeof(ValidationResult<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationResult.WithErrors))!
                .Invoke(null, new object?[] { errors })!;

            return (TResult)validationResult;
        }
    }
}