using MediatR;
using SharedKenel;

namespace StockGuard.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
