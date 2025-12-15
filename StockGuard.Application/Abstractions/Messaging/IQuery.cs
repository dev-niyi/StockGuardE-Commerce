using MediatR;
using SharedKenel;

namespace StockGuard.Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>; 

