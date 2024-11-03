using FluentValidation;
using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Orders.Commands.CreateOrder;
public class PaymentSummaryValidator : AbstractValidator<PaymentSummary>
{
    public PaymentSummaryValidator()
    {
        RuleFor(x => x.CardLast4)
                .InclusiveBetween(1000, 9999)
                .WithMessage(ResourceErrorMessages.EMPTY_PAYMENT_CARD_LAST4);

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PAYMENT_CARD_BRAND)
            .MinimumLength(2).WithMessage(ResourceErrorMessages.INVALID_CARD_BRAND)
            .MaximumLength(50).WithMessage(ResourceErrorMessages.INVALID_CARD_BRAND);

        RuleFor(x => x.ExpMonth)
            .InclusiveBetween(1, 12).WithMessage(ResourceErrorMessages.INVALID_CARD_EXPMONTH);

        RuleFor(x => x.ExpYear)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage(ResourceErrorMessages.INVALID_CARD_YEAR);
    }
}
