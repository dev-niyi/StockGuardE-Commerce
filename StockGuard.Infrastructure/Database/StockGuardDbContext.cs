using StockGuard.Domain;
using Microsoft.EntityFrameworkCore;
using StockGuard.Application.Abstractions.Data;
using StockGuard.Domain.Orders;
using StockGuard.Domain.OrderItems;

namespace StockGuard.Infrastructure.Database;

public sealed class StockGuardDbContext(
	DbContextOptions<StockGuardDbContext> options) : DbContext(options), IApplicationDbContext
{
	public DbSet<Product> Products { get; set; }

	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		int result = await base.SaveChangesAsync(cancellationToken);
		return result;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockGuardDbContext).Assembly);
		//modelBuilder.HasDefaultSchema(Schemas.Default);
	}
}
