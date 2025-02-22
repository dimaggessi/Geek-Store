using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Payments.Commands.RefundOrder
{
    public class RefundOrderValidator : AbstractValidator<RefundOrderCommand>
    {
        public RefundOrderValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .NotNull()
                .WithMessage(ResourceErrorMessages.EMPTY_ORDER_ID);
        }
    }
}
