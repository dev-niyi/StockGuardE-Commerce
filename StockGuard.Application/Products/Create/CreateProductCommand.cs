using StockGuard.Domain.Products;
using StockGuard.Application.Abstractions.Messaging;

namespace StockGuard.Application.Products.Create;
public sealed record CreateProductCommand
	(
		string Name,
		string Description,
		string ImageUrl,
		decimal Price,
		int StockQuantity,
		string Brand,
		AvailabilityStatus Availability
	) : ICommand<Guid>;