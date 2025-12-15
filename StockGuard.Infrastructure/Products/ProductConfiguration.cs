using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockGuard.Domain;

namespace StockGuard.Infrastructure.Products;

internal sealed class ProductionConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Name)
			.HasMaxLength(200)
			.IsRequired();

		builder.Property(p => p.Brand)
			.HasMaxLength(200);

		builder.Property(p => p.Price)
			.HasPrecision(22, 2)
			.IsRequired();

		builder.Property(p => p.Description)
			.HasMaxLength(300)
			.IsRequired();

		builder.Property(P => P.StockQuantity)
			.IsRequired();
	}
}
