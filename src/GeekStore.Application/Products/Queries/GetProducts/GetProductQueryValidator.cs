using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Products.Queries.GetProducts;
public class GetProductQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.REQUIRED_PAGE_INDEX)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.MIN_PAGE_INDEX);
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(10)
            .WithMessage(ResourceErrorMessages.INTERVAL_PAGE_SIZE);
    }
}
