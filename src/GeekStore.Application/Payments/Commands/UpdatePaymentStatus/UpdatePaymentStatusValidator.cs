using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Payments.Commands.UpdatePaymentStatus
{
    public class UpdatePaymentStatusValidator : AbstractValidator<UpdatePaymentStatusCommand>
    {
        public UpdatePaymentStatusValidator()
        {
            RuleFor(x => x.PaymentIntent)
                .NotEmpty()
                .NotNull()
                .WithMessage(ResourceErrorMessages.EMPTY_PAYMENT_INTENT);
        }
    }
}
