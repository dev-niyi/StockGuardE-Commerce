namespace StockGuard.Application.Orders.Create;

public record CreateOrderResponse
{
	public Guid OrderId { get; init; }
	public DateTime OrderDate { get; init; }
	public decimal TotalAmount { get; init; }
	public string Status { get; init; } = string.Empty;
	public List<OrderItemResponse> Items { get; init; } = new();
}
