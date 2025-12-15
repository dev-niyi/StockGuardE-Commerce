using StockGuard.Application.Abstractions.Messaging;

namespace StockGuard.Application.Products.Delete;

public sealed record DeleteProductCommand(Guid ProductId) : ICommand;
