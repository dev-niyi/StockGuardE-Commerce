using SharedKenel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockGuard.Application.Abstractions.Data;
using StockGuard.Application.Abstractions.Messaging;

namespace StockGuard.Application.Products.Get;

internal sealed class GetProductsQueryHandler(
	IApplicationDbContext context,
	ILogger<GetProductsQueryHandler> logger)
	: IQueryHandler<GetProductsQuery, List<ProductsResponse>>
{
	public async Task<Result<List<ProductsResponse>>> Handle(GetProductsQuery query,
		CancellationToken cancellationToken)
	{
		List<ProductsResponse> products = await context.Products
			.Where(productItem => productItem.IsDeleted == false)
			.Select(productItem => new ProductsResponse
			{
				Id = productItem.Id,
				Name = productItem.Name,
				Description = productItem.Description,
				Price = productItem.Price,
				Availability = productItem.Availability,
				Brand = productItem.Brand,
				//CategoryId = productItem.CategoryId,
			})
			.AsNoTracking()
			.ToListAsync(cancellationToken);

		if (!products.Any())
		{
			logger.LogInformation("No Product Was Found");
			return Result.Success(
				new List<ProductsResponse>(), "Products Not Found");
		}

		logger.LogInformation("Product Retrieved Successfully");
		return products;
	}
}
