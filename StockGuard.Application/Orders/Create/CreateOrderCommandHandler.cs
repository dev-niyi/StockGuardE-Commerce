using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKenel;
using StockGuard.Application.Abstractions.Data;
using StockGuard.Application.Abstractions.Messaging;
using StockGuard.Application.Products.Create;
using StockGuard.Domain.OrderItems;
using StockGuard.Domain.Orders;
using StockGuard.Domain.Products;
using StockGuard.SharedKernel;

namespace StockGuard.Application.Orders.Create;

internal sealed class CreateOrderCommandHandler(
	IApplicationDbContext context,
	ILogger<CreateProductCommandHandler> logger,
	IDateTimeProvider dateTimeProvider)
   : ICommandHandler<CreateOrderCommand, CreateOrderResponse>
{
	public async Task<Result<CreateOrderResponse>> Handle(
	CreateOrderCommand command,
	CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating order with {ItemCount} items", command.Items.Count);

		if (command.Items is null || !command.Items.Any())
		{
			logger.LogWarning("Order creation failed: Empty order");
			return Result.Failure<CreateOrderResponse>(OrderErrors.EmptyOrder());
		}

		foreach (var item in command.Items)
		{
			if (item.Quantity <= 0)
			{
				logger.LogWarning("Invalid Quantity For Product {ProductId}", item.ProductId);
				return Result.Failure<CreateOrderResponse>(OrderErrors.InvalidQuantity(item.ProductId));
			}
		}

		using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

		try
		{
			var productIds = command.Items
					.Select(i => i.ProductId).ToList();

			var products = await context.Products
				.Where(p => productIds.Contains(p.Id))
				.ToListAsync(cancellationToken);

			if (products.Count != productIds.Distinct().Count())
			{
				var foundIds = products.Select(p => p.Id).ToHashSet();
				var missingId = productIds.FirstOrDefault(id => !foundIds.Contains(id));
				logger.LogWarning("Product {ProductId} not found", missingId);
				return Result.Failure<CreateOrderResponse>(OrderErrors.ProductNotFound(missingId));
			}

			var orderItems = new List<OrderItem>();
			decimal totalAmount = 0;

			foreach (var item in command.Items)
			{
				var product = products.First(p => p.Id == item.ProductId);

				if (product.StockQuantity < item.Quantity)
				{
					logger.LogWarning(
						"Insufficient stock for product {ProductName}. Available: {Available}, Requested: {Requested}",
						product.Name, product.StockQuantity, item.Quantity);

					return Result.Failure<CreateOrderResponse>(
						OrderErrors.InsufficientStock(product.Name, product.StockQuantity, item.Quantity));
				}

				else if(product.Availability == AvailabilityStatus.OutOfStock)
				{
					logger.LogWarning(
						"Product with ID {ProductId} is out of stock", product.Id);

					Result.Failure<CreateOrderResponse>(
						OrderErrors.OutOfStock(product.Id));
				}

				var subtotal = product.Price * item.Quantity;
				totalAmount += subtotal;

				var orderItem = new OrderItem
				{
					Id = Guid.NewGuid(),
					ProductId = product.Id,
					Quantity = item.Quantity,
					UnitPrice = product.Price,
					Subtotal = subtotal
				};

				orderItems.Add(orderItem);


				product.StockQuantity -= item.Quantity;
				product.UpdatedAt = dateTimeProvider.UtcNow;
			}


			var order = new Order
			{
				Id = Guid.NewGuid(),
				OrderedAt = dateTimeProvider.UtcNow,
				TotalAmount = totalAmount,
				Status = OrderStatus.Completed,
				CreatedAt = dateTimeProvider.UtcNow,
				OrderItems = orderItems
			};

			foreach (var item in orderItems)
			{
				item.OrderId = order.Id;
			}

			context.Orders.Add(order);
			await context.SaveChangesAsync(cancellationToken);

			await transaction.CommitAsync(cancellationToken);

			logger.LogInformation("Order {OrderId} Created Successfully With Total Amount {TotalAmount}",
				order.Id, order.TotalAmount);

			var response = new CreateOrderResponse
			{
				OrderId = order.Id,
				OrderDate = order.OrderedAt,
				TotalAmount = order.TotalAmount,
				Status = order.Status.ToString(),
				Items = orderItems.Select(oi => new OrderItemResponse
				{
					ProductId = oi.ProductId,
					ProductName = products.First(p => p.Id == oi.ProductId).Name,
					Quantity = oi.Quantity,
					UnitPrice = oi.UnitPrice,
					Subtotal = oi.Subtotal
				}).ToList()
			};

			return Result.Success(response);
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync(cancellationToken);
			logger.LogError(ex, "Error Creating Order");
			throw;
		}
	}
}






