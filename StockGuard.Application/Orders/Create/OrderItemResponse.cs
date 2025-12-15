namespace StockGuard.Application.Orders.Create;

public record OrderItemResponse
{
	public Guid ProductId { get; init; }
	public string ProductName { get; init; } = string.Empty;
	public int Quantity { get; init; }
	public decimal UnitPrice { get; init; }
	public decimal Subtotal { get; init; }
}
