using System.Windows.Input;
using MediatR;
using StockGuard.Application.Abstractions.Messaging;

namespace StockGuard.Application.Orders.Create;

public record CreateOrderCommand : ICommand<CreateOrderResponse>
{
	public List<OrderItemDto> Items { get; init; } = new();
}

