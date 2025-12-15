using FluentValidation;
using FluentValidation.Results;
using SharedKenel;
using StockGuard.Application.Abstractions.Messaging;
using StockGuard.SharedKernel;

namespace StockGuard.Application.Abstractions.Behaviour;

internal static class ValidationDecorator
{
	internal sealed class CommandBaseHandler<TCommand>(ICommandHandler<TCommand> handler, IEnumerable<IValidator<TCommand>> validators)
		: ICommandHandler<TCommand>
		where TCommand : ICommand
	{
		public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
		{
			ValidationFailure[] failures = await ValidateCommandAsync(command, validators);
			if (failures.Length == 0)
			{
				return await handler.Handle(command, cancellationToken);
			}
			return Result.Failure(Error.Validation(CreateValidationError(failures)));
		}
	}

	internal sealed class CommandHandler<TCommand, TResponse>(ICommandHandler<TCommand, TResponse> handler, IEnumerable<IValidator<TCommand>> validators)
		: ICommandHandler<TCommand, TResponse>
		where TCommand : ICommand<TResponse>
	{
		public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
		{
			ValidationFailure[] failures = await ValidateCommandAsync(command, validators);
			if (failures.Length == 0)
			{
				return await handler.Handle(command, cancellationToken);
			}

			return Result.Failure<TResponse>(Error.Validation(CreateValidationError(failures)));
		}
	}


	private static async Task<ValidationFailure[]> ValidateCommandAsync<TCommand>(TCommand command, IEnumerable<IValidator<TCommand>> validators)
	{
		if (!validators.Any())
		{
			return [];
		}

		var context = new ValidationContext<TCommand>(command);

		ValidationResult[] validationResults = await Task.WhenAll(
			validators.Select(validator => validator.ValidateAsync(context)));

		ValidationFailure[] validationFailures = validationResults
			.Where(validationResult => !validationResult.IsValid)
			.SelectMany(validationResult => validationResult.Errors)
			.ToArray();

		return validationFailures;
	}
	private static ValidationError CreateValidationError(ValidationFailure[] failures) =>
		new(failures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage)).ToArray());
}
