using StockGuard.SharedKernel;

namespace SharedKenel;

public record Error
{
	public string Code { get; }
	public string Description { get; }
	public ErrorType Type { get; }


	public Error(string code, string description, ErrorType errorType)
	{
		Code = code;
		Description = description;
		Type = errorType;
	}

	public static readonly Error None = new Error(null, null, ErrorType.None);
	public static readonly Error Nullvalue = new Error("General Null Value", "Null Value Was Provided", ErrorType.Failure);

	public static Error Failure(string code, string description) =>
		new(code, description, ErrorType.Failure);
	public static Error Empty(string code, string description) =>
	new(code, description, ErrorType.Failure);
	public static Error BadRequest(string code, string description) =>
		new(code, description, ErrorType.Failure);
	public static Error Validation(string code, string description) =>
		new(code, description, ErrorType.Failure);
	public static Error Problem(string code, string description) =>
		new(code, description, ErrorType.Problem);
	public static Error NotFound(string code, string description) =>
		new(code, description, ErrorType.Notfound);
	public static Error Conflict(string code, string description) =>
		new(code, description, ErrorType.Failure);
	public static Error Validation(ValidationError validationError) =>
	new Error("Validation.Error", "One or more validation failures occurred.", ErrorType.Validation);
}

