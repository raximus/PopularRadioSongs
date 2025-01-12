using FluentValidation.Results;

namespace PopularRadioSongs.Application.Results
{
    public class UseCaseResult<T> : UseCaseResult
    {
        public T Value { get; }

        protected UseCaseResult(T value) : base(null)
        {
            Value = value;
        }
        protected UseCaseResult(IFailure failure) : base(failure)
        {
            Value = default!;
        }

        public static UseCaseResult<T> Success(T value) => new UseCaseResult<T>(value);
        public static new UseCaseResult<T> BadRequest(string message) => new UseCaseResult<T>(new BadRequestFailure(message));
        public static new UseCaseResult<T> NotFound(string message) => new UseCaseResult<T>(new NotFoundFailure(message));
        public static new UseCaseResult<T> ValidationError(ValidationResult validationResult) => new UseCaseResult<T>(new ValidationFailure(validationResult.ToDictionary()));
    }
}