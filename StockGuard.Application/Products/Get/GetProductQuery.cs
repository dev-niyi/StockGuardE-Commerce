using StockGuard.Application.Abstractions.Messaging;

namespace StockGuard.Application.Products.Get;

public sealed record GetProductsQuery : IQuery<List<ProductsResponse>>;
