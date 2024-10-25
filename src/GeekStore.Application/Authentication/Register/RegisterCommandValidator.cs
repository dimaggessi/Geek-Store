using FluentValidation;

namespace GeekStore.Application.Authentication.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .Length(2, 50)
            .WithMessage("O nome deve conter de 2 a 50 caracteres.");
        RuleFor(u => u.LastName)
            .NotEmpty()
            .WithMessage("O sobrenome é obrigatório.")
            .Length(2, 50)
            .WithMessage("O sobrenome deve conter de 2 a 50 caracteres.");
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("O email é obrigatório.")
            .EmailAddress()
            .WithMessage("O email deve ter um formato válido.");
        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("A senha é obrigatória.")
            .MinimumLength(6)
            .WithMessage("A senha deve conter no mínimo 6 caracteres.")
            .Matches("[A-Z]").WithMessage("A senha deve ter pelo menos uma letra maiúscula.")
            .Matches("[a-z]").WithMessage("A senha deve ter pelo menos uma letra minúscula.")
            .Matches("[0-9]").WithMessage("A senha deve ter pelo menos um número.")
            .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um caractere especial.");
    }
}
