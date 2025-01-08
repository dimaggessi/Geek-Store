using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Delivery;
public class GetDeliveryMethodQueryValidator : AbstractValidator<GetDeliveryMethodQuery>
{
    public GetDeliveryMethodQueryValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_SHOPPING_CART_ID);

        RuleFor(d => d.DeliveryMethodId)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_DELIVERY_METHOD_ID);

        RuleFor(d => d.PostalCode)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_POSTAL_CODE)
            .Matches(@"^\d{8}$")
            .WithMessage(ResourceErrorMessages.VALID_POSTAL_CODE);
    }
}