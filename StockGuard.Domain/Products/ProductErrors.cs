using SharedKenel;

namespace StockGuard.Domain.Products;

public static class ProductErrors
{
	public static Error NotFound(Guid productId) => Error.NotFound(
	"Product.Notfound", $"Product {productId} not found");
}
