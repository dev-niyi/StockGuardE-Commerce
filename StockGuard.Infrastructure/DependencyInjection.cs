using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockGuard.Application.Abstractions.Data;
using StockGuard.Infrastructure.Database;
using StockGuard.Infrastructure.Time;
using StockGuard.SharedKernel;

namespace StockGuard.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
		services
		.AddServices()
		.AddDatabase(configuration);

	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
		services.AddScoped<IApplicationDbContext, StockGuardDbContext>();

		return services;
	}

	private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		string? connectionString = configuration.GetConnectionString("DefaultConnection");

		if (string.IsNullOrEmpty(connectionString))
		{
			throw new ArgumentException("Database connection string 'DefaultConnection' is not configured.");
		}

		services.AddDbContext<StockGuardDbContext>(
				options => options.UseSqlServer(connectionString));
		//services.AddDbContext<StockGuardDbContext>(options =>
		//	options.UseSqlServer(
		//		connectionString,
		//		sqlOptions => sqlOptions.MigrationsAssembly("StockGuard.Infrastructure")
		//	));

		return services;
	}
}