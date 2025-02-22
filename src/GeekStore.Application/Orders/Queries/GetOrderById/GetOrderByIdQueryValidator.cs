using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Orders.Queries.GetOrderById;
public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(x => x.OrderId)
            .NotNull()
            .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_ORDER_ID)
            .GreaterThan(0).WithMessage(ResourceErrorMessages.ERROR_INVALID_ID);
    }
}
