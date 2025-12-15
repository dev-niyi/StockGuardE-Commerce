using StockGuard.Domain.OrderItems;

namespace StockGuard.Domain.Orders;

public class Order
{
	public Guid Id { get; set; }
	public decimal TotalAmount { get; set; }
	public OrderStatus Status { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime OrderedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }

	public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
