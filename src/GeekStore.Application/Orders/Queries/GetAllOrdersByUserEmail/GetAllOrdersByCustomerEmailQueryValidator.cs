using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Orders.Queries.GetAllOrdersByUserEmail;
public class GetAllOrdersByCustomerEmailQueryValidator : AbstractValidator<GetAllOrdersByCustomerEmailQuery>
{
    public GetAllOrdersByCustomerEmailQueryValidator()
    {
        RuleFor(x => x.CustomerEmail)
            .NotNull()
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_CUSTOMER_EMAIL);
    }
}
