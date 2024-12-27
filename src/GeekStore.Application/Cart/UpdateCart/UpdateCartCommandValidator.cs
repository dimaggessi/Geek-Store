using FluentValidation;
using GeekStore.Application.Cart.Common;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Cart.UpdateCart;
public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
{
    public UpdateCartCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_SHOPPING_CART_ID);

        RuleForEach(x => x.Items)
            .SetValidator(new CartItemValidator())
            .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_LIST);
    }
}
