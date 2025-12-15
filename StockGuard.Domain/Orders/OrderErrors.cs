using SharedKenel;

namespace StockGuard.Domain.Orders;

public static class OrderErrors
{
	public static Error NotFound(Guid orderId) =>Error.NotFound(
		"Order.NotFound",
		$"Order with ID '{orderId}' was not found");

	public static Error EmptyOrder() => Error.Empty(
		"Order.EmptyOrder",
		"Order must contain at least one item");

	public static Error InsufficientStock(string productName, int available, int requested) => Error.Failure(
		"Order.InsufficientStock",
		$"Insufficient stock for product '{productName}'. Available: {available}, Requested: {requested}");

	public static Error ProductNotFound(Guid productId) => Error.NotFound(
		"Order.ProductNotFound",
		$"Product with ID '{productId}' was not found");

	public static Error InvalidQuantity(Guid productId) => Error.Failure(
		"Order.InvalidQuantity",
		$"Quantity must be greater than zero for product '{productId}'");

	public static Error OutOfStock(Guid productId) => Error.Failure(
		"Order.OutOfStock",
		$"Product with ID '{productId}' is out of stock,"
		);
}
