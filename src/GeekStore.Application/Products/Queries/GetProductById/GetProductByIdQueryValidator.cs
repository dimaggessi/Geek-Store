using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Products.Queries.GetProductById;
public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_ID);
    }
}
