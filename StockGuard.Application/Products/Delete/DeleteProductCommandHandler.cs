using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKenel;
using StockGuard.Application.Abstractions.Data;
using StockGuard.Application.Abstractions.Messaging;
using StockGuard.Domain.Products;
using StockGuard.SharedKernel;

namespace StockGuard.Application.Products.Delete;
internal sealed class DeleteProductCommandHandler(
	IApplicationDbContext context,
	IDateTimeProvider dateTimeProvider,
	ILogger<DeleteProductCommandHandler> logger)
	: ICommandHandler<DeleteProductCommand>
{
	public async Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
	{
		var product = await context.Products.
			SingleOrDefaultAsync(
			p => p.Id == command.ProductId,
			cancellationToken);

		if (product is null)
		{
			logger.LogWarning("Product {ProductId} not found", command.ProductId);
			return Result.Failure(ProductErrors.NotFound(command.ProductId));
		}

		if (product.IsDeleted)
		{
			logger.LogInformation("Product {ProductId} has already been deleted", command.ProductId);
			return Result.Success();
		}

		product.IsDeleted = true;
		product.DeletedAt = dateTimeProvider.UtcNow;
		context.Products.Update(product);

		await context.SaveChangesAsync(cancellationToken);

		logger.LogInformation("Product {ProductId} deleted successfully", product.Id);
		return Result.Success("Product deleted successfully");
	}
}
