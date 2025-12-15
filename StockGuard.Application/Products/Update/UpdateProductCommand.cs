using System.Text.Json.Serialization;
using SharedKenel;
using StockGuard.Application.Abstractions.Messaging;
using StockGuard.Domain.Products;

namespace StockGuard.Application.Products.Update;

public record UpdateProductCommand : ICommand<UpdateProductResponse>
{
	[JsonIgnore]
	public Guid ProductId { get; init; }

	public string Name { get; init; }
	public string Description { get; init; }
	public decimal Price { get; init; }
	public int StockQuantity { get; init; }
	public string ImageUrl { get; init; }
	public string Brand { get; init; }
	public AvailabilityStatus Availability { get; init; }
}
