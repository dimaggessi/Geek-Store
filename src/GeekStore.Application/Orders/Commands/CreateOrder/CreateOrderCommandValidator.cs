using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerEmail)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_CUSTOMER_EMAIL)
                .EmailAddress().WithMessage(ResourceErrorMessages.EMPTY_CUSTOMER_EMAIL);

            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_SHOPPING_CART_ID);

            RuleFor(x => x.DeliveryMethodId)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_DELIVERY_METHOD_ID)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.ERROR_INVALID_ID);

            RuleFor(x => x.ShippingAddress)
                .NotNull().WithMessage(ResourceErrorMessages.ADDRESS_NULL)
                .SetValidator(new ShippingAddressValidator());

            RuleFor(x => x.PaymentSummary)
                .NotNull().WithMessage(ResourceErrorMessages.PAYMENT_SUMMARY_NOT_NULL)
                .SetValidator(new PaymentSummaryValidator());
        }
    }
}