using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Cart.CreateCart;
public class GetOrCreateCartValidator : AbstractValidator<GetOrCreateCart>
{
    public GetOrCreateCartValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_SHOPPING_CART_ID);
    }
}
