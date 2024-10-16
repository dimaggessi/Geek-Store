using FluentValidation;

namespace GeekStore.Application.Products.Queries.GetProducts;
public class GetProductQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .NotEmpty()
            .WithMessage("Page index is required")
            .GreaterThan(0)
            .WithMessage("Page index must be greater than zero.");
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(10)
            .WithMessage("Page size must be between 1 and 10.");
    }
}
