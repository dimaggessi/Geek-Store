using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Products.Commands.UpdateProduct;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_ID);
        }
}
