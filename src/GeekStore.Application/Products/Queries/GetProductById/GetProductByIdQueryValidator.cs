using FluentValidation;

namespace GeekStore.Application.Products.Queries.GetProductById;
public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product Id is required.");
    }
}
