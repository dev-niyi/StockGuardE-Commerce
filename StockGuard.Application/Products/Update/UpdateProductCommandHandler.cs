using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKenel;
using StockGuard.Application.Abstractions.Data;
using StockGuard.Application.Abstractions.Messaging;
using StockGuard.Domain.Products;
using StockGuard.SharedKernel;

namespace StockGuard.Application.Products.Update;

internal sealed class UpdateProductCommandHandler(
	IApplicationDbContext context,
	IDateTimeProvider dateTimeProvider,
	ILogger<UpdateProductCommandHandler> logger)
	: ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
	public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
	{
		var product = await context.Products
			.SingleOrDefaultAsync(productItem => productItem.Id == command.ProductId, cancellationToken);

		if (product is null)
		{
			logger.LogWarning("Product {ProductId} Not Found", product.Id);
			return Result.Failure<UpdateProductResponse>
					(ProductErrors.NotFound(command.ProductId));
		}

		product.Name = command.Name;
		product.Description = command.Description;
		product.Price = command.Price;
		product.StockQuantity = command.StockQuantity;
		product.ImageUrl = command.ImageUrl;
		product.Brand = command.Brand;
		product.Availability = command.Availability;
		product.UpdatedAt = dateTimeProvider.UtcNow;

		context.Products.Update(product);


		await context.SaveChangesAsync(cancellationToken);
		logger.LogInformation("Product {ProductId} Updated Successfully", command.ProductId);

		var updatedProduct = new UpdateProductResponse
		{
			Id = product.Id,
			Name = product.Name,
			Description = product.Description,
			StockQuantity = product.StockQuantity,
			ImageUrl = product.ImageUrl,
			Price = product.Price,
			Brand = product.Brand,
			Availability = product.Availability,
			//CategoryId = product.CategoryId,
		};

		return Result.Success(updatedProduct);
	}
}
