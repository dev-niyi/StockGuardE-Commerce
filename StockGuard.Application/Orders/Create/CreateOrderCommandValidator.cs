using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace StockGuard.Application.Orders.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
	public CreateOrderCommandValidator()
	{
		RuleFor(x => x.Items)
			.NotEmpty().WithMessage("Order must contain at least one item")
			.Must(items => items != null && items.Any()).WithMessage("Order items cannot be null or empty");

		RuleForEach(x => x.Items).ChildRules(item =>
		{
			item.RuleFor(x => x.ProductId)
				.NotEmpty().WithMessage("Product ID is required");

			item.RuleFor(x => x.Quantity)
				.GreaterThan(0).WithMessage("Quantity must be greater than zero");
		});
	}
}

