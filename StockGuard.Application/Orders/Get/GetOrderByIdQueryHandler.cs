using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKenel;
using StockGuard.Application.Abstractions.Data;
using StockGuard.Application.Abstractions.Messaging;
using StockGuard.Application.Products.Create;
using StockGuard.Domain.Orders;

namespace StockGuard.Application.Orders.Get;

internal sealed class GetOrderByIdQueryHandler(
	IApplicationDbContext context,
	ILogger<CreateProductCommandHandler> logger)
	: IQueryHandler<GetOrderByIdQuery, OrderDetailResponse>
{
	public async Task<Result<OrderDetailResponse>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting order {OrderId}", query.OrderId);

		var order = await context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.AsNoTracking()
			.FirstOrDefaultAsync(o => o.Id == query.OrderId, cancellationToken);

		if (order is null)
		{
			logger.LogWarning("Order {OrderId} not found", query.OrderId);
			return Result.Failure<OrderDetailResponse>(OrderErrors.NotFound(query.OrderId));
		}

		var response = new OrderDetailResponse
		{
			Id = order.Id,
			OrderDate = order.OrderedAt,
			TotalAmount = order.TotalAmount,
			Status = order.Status.ToString(),
			CreatedAt = order.CreatedAt,
			Items = order.OrderItems.Select(oi => new OrderItemDetailResponse
			{
				ProductId = oi.ProductId,
				ProductName = oi.Product.Name,
				Quantity = oi.Quantity,
				UnitPrice = oi.UnitPrice,
				Subtotal = oi.Subtotal
			}).ToList()
		};

		return Result.Success(response);
	}
}

