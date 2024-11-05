using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Orders.Queries.GetAllOrdersByUserEmail;
public class GetAllOrdersValidator : AbstractValidator<GetAllOrdersQuery>
{
    public GetAllOrdersValidator()
    {
        RuleFor(x => x.CustomerEmail)
            .NotNull()
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_CUSTOMER_EMAIL);
    }
}
