using StockGuard.Domain.Products;

namespace StockGuard.Application.Products.Update;
public sealed record UpdateProductResponse
{
	public Guid Id { get; init; }
	public string Name { get; init; }
	public string Description { get; init; }
	public int StockQuantity { get; set; }
	public string ImageUrl { get; init; }
	public decimal Price { get; init; }
	public string Brand { get; init; }
	public AvailabilityStatus Availability { get; set; }
	//public Guid CategoryId { get; init; }
}
