using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StockGuard.Domain;
using StockGuard.Domain.OrderItems;
using StockGuard.Domain.Orders;

namespace StockGuard.Application.Abstractions.Data;

public interface IApplicationDbContext
{
	DbSet<Order> Orders { get; }
	DbSet<OrderItem> OrderItems { get; }
	DbSet<Product> Products { get; }
	DatabaseFacade Database { get; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
