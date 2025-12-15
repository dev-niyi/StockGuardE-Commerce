namespace StockGuard.Application.Orders.Get;

public record OrderDetailResponse
{
	public Guid Id { get; init; }
	public DateTime OrderDate { get; init; }
	public decimal TotalAmount { get; init; }
	public string Status { get; init; } = string.Empty;
	public DateTime CreatedAt { get; init; }
	public List<OrderItemDetailResponse> Items { get; init; } = new();
}
