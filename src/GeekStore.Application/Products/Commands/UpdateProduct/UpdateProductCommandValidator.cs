using FluentValidation;
using GeekStore.Application.Products.Commands.Common;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Products.Commands.UpdateProduct;
public class UpdateProductCommandValidator : ProductCommandValidatorBase<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_ID);
    }
}
