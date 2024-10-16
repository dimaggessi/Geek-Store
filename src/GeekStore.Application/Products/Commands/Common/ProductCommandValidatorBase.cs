using FluentValidation;
using GeekStore.Domain.Shared;

namespace GeekStore.Application.Products.Commands.Common
{
    public abstract class ProductCommandValidatorBase<T> : AbstractValidator<T> where T : ProductCommandBase
    {
        protected ProductCommandValidatorBase()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_NAME)
                .MaximumLength(100).WithMessage(ResourceErrorMessages.MAX_PRODUCT_NAME);
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_DESCRIPTION)
                .MaximumLength(300).WithMessage(ResourceErrorMessages.MAX_PRODUCT_DESCRIPTION);
            RuleFor(p => p.Picture)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_PICTURE);
            RuleFor(p => p.Type)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_TYPE)
                .MaximumLength(100).WithMessage(ResourceErrorMessages.MAX_PRODUCT_TYPE);
            RuleFor(p => p.Brand)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_BRAND)
                .MaximumLength(100).WithMessage(ResourceErrorMessages.MAX_PRODUCT_BRAND);
            RuleFor(p => p.Quantity)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_QUANTITY)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_QUANTITY);
            RuleFor(p => p.Price)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_PRICE)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_PRICE);
            RuleFor(p => p.Width)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_WIDTH)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_WIDTH);
            RuleFor(p => p.Height)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_HEIGHT)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_HEIGHT);
            RuleFor(p => p.Length)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_LENGTH)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_LENGTH);
            RuleFor(p => p.Weight)
                .NotNull().WithMessage(ResourceErrorMessages.EMPTY_PRODUCT_WEIGHT)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.MIN_PRODUCT_WEIGHT);
        }
    }
}