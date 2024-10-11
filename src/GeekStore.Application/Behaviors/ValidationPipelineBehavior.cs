using FluentValidation;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse : IResult
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

            var errorMessages = result.Errors.Select(e => e.ErrorMessage);
            var error = new ValidationError(errorMessages);
            

            return (TResponse)(object)Result<TResponse>.Failure(error);
        }
    }
}