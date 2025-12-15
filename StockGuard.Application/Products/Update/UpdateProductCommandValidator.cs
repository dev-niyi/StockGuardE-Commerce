using FluentValidation;

namespace StockGuard.Application.Products.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
	public UpdateProductCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Product name is required")
			.MaximumLength(200);

		RuleFor(x => x.Description)
			.NotEmpty()
			.MaximumLength(1000);

		RuleFor(x => x.Price)
			.GreaterThan(0).WithMessage("Price must be greater than zero");

		RuleFor(x => x.StockQuantity)
			.GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative");
	}
}