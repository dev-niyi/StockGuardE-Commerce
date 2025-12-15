using StockGuard.Application.Abstractions.Messaging;

namespace StockGuard.Application.Orders.Get;

public record GetOrderByIdQuery(Guid OrderId) : IQuery<OrderDetailResponse>;

