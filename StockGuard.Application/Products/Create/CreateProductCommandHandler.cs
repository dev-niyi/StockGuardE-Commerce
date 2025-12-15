using SharedKenel;
using Microsoft.Extensions.Logging;
using StockGuard.Application.Abstractions.Data;
using StockGuard.SharedKernel;
using StockGuard.Application.Abstractions.Messaging;
using StockGuard.Domain;

namespace StockGuard.Application.Products.Create;

internal sealed class CreateProductCommandHandler(
	IApplicationDbContext context,
	ILogger<CreateProductCommandHandler> logger,
	IDateTimeProvider dateTimeProvider)
	: ICommandHandler<CreateProductCommand, Guid>
{
	public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating Product");

		Product product = new ()
		{
			Name = command.Name,
			Description = command.Description,
			ImageUrl = command.ImageUrl,
			Price = command.Price,
			Brand = command.Brand,
			StockQuantity = command.StockQuantity, 
			CreatedAt = dateTimeProvider.UtcNow
		};

		context.Products.Add(product);
		await context.SaveChangesAsync(cancellationToken);

		logger.LogInformation("Product {ProductId} created successfully", product.Id);
		return Result.Success(product.Id, "Product Created Successfully");
	}
}