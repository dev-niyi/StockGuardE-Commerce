using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockGuard.Domain.Orders;
using StockGuard.SharedKernel;

namespace StockGuard.Infrastructure.Orders;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.HasKey(o => o.Id);

		builder.Property(o => o.TotalAmount)
			.HasPrecision(18, 2)
			.IsRequired();

		builder.Property(o => o.Status)
			.HasConversion<string>()
			.HasMaxLength(50);

		builder.HasMany(o => o.OrderItems)
			.WithOne(oi => oi.Order)
			.HasForeignKey(oi => oi.OrderId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(o => o.OrderedAt);
	}
}
