using StockGuard.SharedKernel;

namespace StockGuard.Infrastructure.Time;

internal class DateTimeProvider : IDateTimeProvider
{
	public DateTime UtcNow => DateTime.UtcNow.AddHours(1);
}

