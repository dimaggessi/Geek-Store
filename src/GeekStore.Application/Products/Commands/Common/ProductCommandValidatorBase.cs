using FluentValidation;

namespace GeekStore.Application.Products.Commands.Common
{
    public abstract class ProductCommandValidatorBase<T> : AbstractValidator<T> where T : ProductCommandBase
    {
        protected ProductCommandValidatorBase()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(300).WithMessage("Product description must not exceed 300 characters.");
            RuleFor(p => p.Picture)
                .NotEmpty().WithMessage("Product picture is required.");
            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("Product type is required.")
                .MaximumLength(100).WithMessage("Product type must not exceed 100 characters.");
            RuleFor(p => p.Brand)
                .NotEmpty().WithMessage("Product brand is required.")
                .MaximumLength(100).WithMessage("Product brand must not exceed 100 characters.");
            RuleFor(p => p.Quantity)
                .NotNull().WithMessage("Product quantity is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Product quantity cannot be negative.");
            RuleFor(p => p.Price)
                .NotNull().WithMessage("Product price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(p => p.Width)
                .NotNull().WithMessage("Product width is required.")
                .GreaterThan(0).WithMessage("Product width must be greater than zero.");
            RuleFor(p => p.Height)
                .NotNull().WithMessage("Product height is required.")
                .GreaterThan(0).WithMessage("Product height must be greater than zero.");
            RuleFor(p => p.Length)
                .NotNull().WithMessage("Product length is required.")
                .GreaterThan(0).WithMessage("Product length must be greater than zero.");
            RuleFor(p => p.Weight)
                .NotNull().WithMessage("Product height is required.")
                .GreaterThan(0).WithMessage("Product weight must be greater than zero.");
        }
    }
}