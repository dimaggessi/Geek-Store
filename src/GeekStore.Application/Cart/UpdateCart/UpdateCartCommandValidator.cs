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

        RuleFor(c => c.DeliveryMethodId)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_DELIVERY_METHOD_ID);

        RuleFor(c => c.PostalCode)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_POSTAL_CODE)
            .Matches(@"^\d{8}$")
            .WithMessage(ResourceErrorMessages.VALID_POSTAL_CODE);

        RuleForEach(x => x.Items)
            .SetValidator(new CartItemValidator())
            .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_LIST);
    }
}
