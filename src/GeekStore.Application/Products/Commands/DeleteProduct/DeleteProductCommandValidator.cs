using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Products.Commands.DeleteProduct;
public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_ID);
    }
}
