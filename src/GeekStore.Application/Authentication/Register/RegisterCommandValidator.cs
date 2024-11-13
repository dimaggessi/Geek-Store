using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Authentication.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_USER_FIRST_NAME)
            .Length(2, 50)
            .WithMessage(ResourceErrorMessages.ERROR_USER_FIRST_NAME_LENGTH);
        RuleFor(u => u.LastName)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_USER_LAST_NAME)
            .Length(2, 50)
            .WithMessage(ResourceErrorMessages.ERROR_USER_LAST_NAME_LENGTH);
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_USER_EMAIL)
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.ERROR_INVALID_EMAIL);
        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_USER_PASSWORD)
            .MinimumLength(6)
            .WithMessage(ResourceErrorMessages.ERROR_INVALID_PASSWORD_LENGTH)
            .Matches("[A-Z]").WithMessage(ResourceErrorMessages.ERROR_INVALID_PASSWORD_UPPERCASE)
            .Matches("[a-z]").WithMessage(ResourceErrorMessages.ERROR_INVALID_PASSWORD_LOWERCASE)
            .Matches("[0-9]").WithMessage(ResourceErrorMessages.ERROR_INVALID_PASSWORD_NUMBER)
            .Matches("[^a-zA-Z0-9]").WithMessage(ResourceErrorMessages.ERROR_INVALID_PASSWORD_SPECIAL);
    }
}
