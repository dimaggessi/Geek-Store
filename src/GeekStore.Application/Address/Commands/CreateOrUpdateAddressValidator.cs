using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Address.Commands;
public class CreateOrUpdateAddressValidator : AbstractValidator<CreateOrUpdateAddressCommand>
{
    public CreateOrUpdateAddressValidator()
    {
        RuleFor(a => a.Number)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_ADDRESS_NUMBER);
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
