using StockGuard.Application.Abstractions.Messaging;

namespace StockGuard.Application.Products.GetById;

public sealed record GetProductByIdQuery(Guid ProductId) : IQuery<ProductResponse>;

