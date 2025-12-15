using SharedKenel;

namespace StockGuard.SharedKernel;

public sealed class ValidationError
{
	public ValidationError(Error[] errors)
	{
		Errors = errors;
	}

	public Error[] Errors { get; }
}