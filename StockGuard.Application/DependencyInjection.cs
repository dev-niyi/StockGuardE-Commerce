using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockGuard.Application.Abstractions.Behaviour;
using StockGuard.Application.Abstractions.Messaging;

namespace StockGuard.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
			.AddClasses(classes => classes.AssignableToAny(typeof(ICommandHandler<>)), publicOnly: false)
				.AsImplementedInterfaces()
				.WithScopedLifetime()
			.AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
				.AsImplementedInterfaces()
				.WithScopedLifetime()
			.AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
				.AsImplementedInterfaces()
				.WithScopedLifetime()
		);

		// 3. Apply decorators
		services.Decorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandBaseHandler<>));
		services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
		services.Decorate(typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandBaseHandler<>));
		services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
		services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));

		return services;
	}
}
