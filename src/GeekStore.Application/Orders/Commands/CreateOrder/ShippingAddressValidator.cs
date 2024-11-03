using FluentValidation;
using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Orders.Commands.CreateOrder;
public class ShippingAddressValidator : AbstractValidator<ShippingAddress>
{
    public ShippingAddressValidator()
    {
        RuleFor(a => a.Name)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_ADDRESS_NAME);

        RuleFor(a => a.Street)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.ADDRESS_STREET)
            .MaximumLength(100)
            .WithMessage(ResourceErrorMessages.ADDRESS_STREET_MAX);

        RuleFor(a => a.Neighborhood)
            .MaximumLength(100)
            .WithMessage(ResourceErrorMessages.ADDRESS_NEIGHBORHOOD_MAX);

        RuleFor(a => a.City)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.ADDRESS_CITY)
            .MaximumLength(50)
            .WithMessage(ResourceErrorMessages.ADDRESS_CITY_MAX);

        RuleFor(a => a.State)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.ADDRESS_STATE)
            .MaximumLength(50)
            .WithMessage(ResourceErrorMessages.ADDRESS_STATE_MAX);

        RuleFor(a => a.PostalCode)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.ADDRESS_POSTAL_CODE)
            .MaximumLength(20)
            .WithMessage(ResourceErrorMessages.ADDRESS_POSTAL_CODE_MAX);

        RuleFor(a => a.Country)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.ADDRESS_COUNTRY)
            .MaximumLength(50)
            .WithMessage(ResourceErrorMessages.ADDRESS_COUNTRY_MAX);
    }
}
