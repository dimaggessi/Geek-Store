using FluentValidation;
using GeekStore.Application.Products.Commands.Common;

namespace GeekStore.Application.Products.Commands.UpdateProduct;
public class UpdateProductCommandValidator : ProductCommandValidatorBase<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
    }
}
