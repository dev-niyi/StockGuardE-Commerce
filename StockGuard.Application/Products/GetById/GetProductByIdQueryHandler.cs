using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKenel;
using StockGuard.Application.Abstractions.Data;
using StockGuard.Application.Abstractions.Messaging;
using StockGuard.Domain.Products;

namespace StockGuard.Application.Products.GetById;

internal sealed class GetProductByIdQueryHandler(
	IApplicationDbContext context,
	ILogger<GetProductByIdQueryHandler> logger)
	: IQueryHandler<GetProductByIdQuery, ProductResponse>
	{
		public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
		{
			ProductResponse product = await context.Products
				.Where(productItem => productItem.Id == query.ProductId && !productItem.IsDeleted)
				.Select(productItem => new ProductResponse
				{
					Id = productItem.Id,
					Name = productItem.Name,
					Description = productItem.Description,
					ImageUrl = productItem.ImageUrl,
					Price = productItem.Price,
					Brand = productItem.Brand,
					Availability = productItem.Availability,
					//CategoryId = productItem.CategoryId,
				})
				.AsNoTracking()
				.SingleOrDefaultAsync(cancellationToken);

			if (product is null)
			{
				logger.LogInformation("Product {ProductId} Not Found", product.Id);
				return Result.Failure<ProductResponse>
					(ProductErrors.NotFound(query.ProductId));
			}

			logger.LogInformation("Product {ProductId} Retrieved Successfully", query.ProductId);
			return Result.Success(product);
		}
	}

