using FluentValidation.Results;

namespace PopularRadioSongs.Application.Results
{
    public class UseCaseResult
    {
        public IFailure? Failure { get; }
        public bool IsSuccess => Failure is null;

        protected UseCaseResult(IFailure? failure)
        {
            Failure = failure;
        }

        public static UseCaseResult Success() => new UseCaseResult(null);
        public static UseCaseResult BadRequest(string message) => new UseCaseResult(new BadRequestFailure(message));
        public static UseCaseResult NotFound(string message) => new UseCaseResult(new NotFoundFailure(message));
        public static UseCaseResult ValidationError(ValidationResult validationResult) => new UseCaseResult(new ValidationFailure(validationResult.ToDictionary()));
    }
}