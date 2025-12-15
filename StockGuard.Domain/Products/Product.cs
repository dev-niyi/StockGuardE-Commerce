using StockGuard.Domain.Products;
using StockGuard.SharedKernel;

namespace StockGuard.Domain;

public sealed class Product : Entity
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string Brand { get; set; }
	public decimal Price { get; set; }
	public int StockQuantity { get; set; }
	public string ImageUrl { get; set; }
	public AvailabilityStatus Availability { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
}
