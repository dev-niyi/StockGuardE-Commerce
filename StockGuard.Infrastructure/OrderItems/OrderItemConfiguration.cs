using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockGuard.Domain.OrderItems;

namespace StockGuard.Infrastructure.OrderItems;

internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
	public void Configure(EntityTypeBuilder<OrderItem> builder)
	{
		builder.HasKey(oi => oi.Id);

		builder.Property(oi => oi.UnitPrice)
			.HasPrecision(18, 2)
			.IsRequired();

		builder.Property(oi => oi.Subtotal)
			.HasPrecision(18, 2)
			.IsRequired();


		builder.HasIndex(oi => oi.ProductId);
		builder.HasIndex(oi => oi.OrderId);
	}
}
