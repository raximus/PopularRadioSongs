using FluentValidation.Results;

namespace PopularRadioSongs.Application.Results
{
    public class PagedUseCaseResult<T> : UseCaseResult<T>
    {
        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int TotalItems { get; }

        protected PagedUseCaseResult(int page, int pageSize, int totalItems, T value) : base(value)
        {
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
        }
        protected PagedUseCaseResult(IFailure failure) : base(failure)
        {
        }

        public static PagedUseCaseResult<T> Success(int page, int pageSize, int totalItems, T value) => new PagedUseCaseResult<T>(page, pageSize, totalItems, value);
        public static new PagedUseCaseResult<T> BadRequest(string message) => new PagedUseCaseResult<T>(new BadRequestFailure(message));
        public static new PagedUseCaseResult<T> NotFound(string message) => new PagedUseCaseResult<T>(new NotFoundFailure(message));
        public static new PagedUseCaseResult<T> ValidationError(ValidationResult validationResult) => new PagedUseCaseResult<T>(new ValidationFailure(validationResult.ToDictionary()));
    }
}