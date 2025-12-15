namespace StockGuard.Application.Orders.Create;

public record OrderItemDto
{
	public Guid ProductId { get; init; }
	public int Quantity { get; init; }
}
