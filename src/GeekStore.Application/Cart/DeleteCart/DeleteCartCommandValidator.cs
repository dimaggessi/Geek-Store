using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Cart.DeleteCart;
public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_SHOPPING_CART_ID);
    }
}
