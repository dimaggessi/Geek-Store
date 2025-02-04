using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Payments.Commands.CreateOrUpdatePaymentIntent;
public class CreateOrUpdatePaymentIntentValidator : AbstractValidator<CreateOrUpdatePaymentIntentCommand>
{
    public CreateOrUpdatePaymentIntentValidator()
    {
        RuleFor(x => x.CartId)
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
    }
}