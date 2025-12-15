using FluentValidation;

namespace StockGuard.Application.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
	public CreateProductCommandValidator()
	{
		RuleFor(P => P.Name).NotEmpty();
		RuleFor(p => p.Name).MaximumLength(50);
		RuleFor(p => p.Description).NotEmpty();
		RuleFor(p => p.StockQuantity).GreaterThanOrEqualTo(0);
		RuleFor(p => p.Description).MaximumLength(200);
		RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
		//RuleFor(p => p.CategoryId).NotEmpty();
	}
}
