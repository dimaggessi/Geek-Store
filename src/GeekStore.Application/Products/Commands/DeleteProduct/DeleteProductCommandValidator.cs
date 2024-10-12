using FluentValidation;

namespace GeekStore.Application.Products.Commands.DeleteProduct;
public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("Product Id is required");
    }
}
