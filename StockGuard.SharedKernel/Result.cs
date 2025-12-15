using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharedKenel;

public class Result
{
	//properties of the result 
	public bool IsSuccess { get; }
	[JsonIgnore]//use get to avoid changing
	public bool IsFailed => !IsSuccess;
	public string Message { get; }
	public Error Error { get; }



	public Result(bool isSuccess, string message, Error error)
	{
		if (isSuccess && error != Error.None ||
			!isSuccess && error == Error.None)
		{
			throw new ArgumentException("Invalid Error", nameof(error));
		}

		IsSuccess = isSuccess;
		Message = message;
		Error = error;
	}


	public static Result Success(string message = "") => new Result(true, message, Error.None);
	public static Result Failure(Error error) => new Result(false, null, error);

	public static Result<TValue> Success<TValue>(TValue value, string message = "") => new Result<TValue>(value, true, message, Error.None);
	public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, null, error);
}

public class Result<TValue> : Result
{
	private readonly TValue _value;

	public Result(TValue value, bool isSuccess, string message, Error error)
		: base(isSuccess, message, error)
	{
		_value = value;
	}

	[NotNull]
	public TValue Value => IsSuccess
		? _value!
		: throw new InvalidOperationException("The value of a failure result can't be accessed.");

	public static implicit operator Result<TValue>(TValue value) =>
		value is not null ? Success(value) : Failure<TValue>(Error.Nullvalue);

	//	public static Result<TValue> ValidationFailure(Error error) =>
	//		new(default, false,string.Empty, error);
}

