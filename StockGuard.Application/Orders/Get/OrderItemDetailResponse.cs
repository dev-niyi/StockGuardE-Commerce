using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockGuard.Application.Orders.Get;

public record OrderItemDetailResponse
{
	public Guid ProductId { get; init; }
	public string ProductName { get; init; } = string.Empty;
	public int Quantity { get; init; }
	public decimal UnitPrice { get; init; }
	public decimal Subtotal { get; init; }
}
