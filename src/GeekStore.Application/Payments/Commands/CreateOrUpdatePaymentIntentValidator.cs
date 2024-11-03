using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Payments.Commands;
public class CreateOrUpdatePaymentIntentValidator : AbstractValidator<CreateOrUpdatePaymentIntentCommand>
{
    public CreateOrUpdatePaymentIntentValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_SHOPPING_CART_ID);
    }
}
