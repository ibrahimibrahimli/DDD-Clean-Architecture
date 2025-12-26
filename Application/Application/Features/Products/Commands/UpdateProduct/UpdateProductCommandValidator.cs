using FluentValidation;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Product ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MinimumLength(3).WithMessage("Product name must be at least 3 characters")
                .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required")
                .Length(3).WithMessage("Currency must be 3 characters (e.g., USD, EUR)")
                .Matches("^[A-Z]{3}$").WithMessage("Currency must be in uppercase (e.g., USD)");
        }
    }
}
