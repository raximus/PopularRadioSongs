using FluentValidation.Results;

namespace PopularRadioSongs.Application.Results
{
    public class PagedUseCaseResult<T> : UseCaseResult<T>
    {
        public int PageNumber { get; }

        protected PagedUseCaseResult(int pageNumber, T value) : base(value)
        {
            PageNumber = pageNumber;
        }
        protected PagedUseCaseResult(IFailure failure) : base(failure)
        {
            PageNumber = 0;
        }

        public static PagedUseCaseResult<T> Success(int pageNumber, T value) => new PagedUseCaseResult<T>(pageNumber, value);
        public static new PagedUseCaseResult<T> BadRequest(string message) => new PagedUseCaseResult<T>(new BadRequestFailure(message));
        public static new PagedUseCaseResult<T> NotFound(string message) => new PagedUseCaseResult<T>(new NotFoundFailure(message));
        public static new PagedUseCaseResult<T> ValidationError(ValidationResult validationResult) => new PagedUseCaseResult<T>(new ValidationFailure(validationResult.ToDictionary()));
    }
}