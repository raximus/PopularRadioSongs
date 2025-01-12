using FluentValidation;
using FluentValidation.Results;
using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : UseCaseResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var validationResult = await _validators.First().ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(UseCaseResult<>))
                    {
                        return UseCaseResultOfTResponse(validationResult);
                    }
                    else if (typeof(TResponse) == typeof(UseCaseResult))
                    {
                        return UseCaseResultResponse(validationResult);
                    }

                    throw new ValidationException(validationResult.Errors);
                }
            }

            return await next().ConfigureAwait(false);
        }

        private static TResponse UseCaseResultOfTResponse(ValidationResult validationResult)
        {
            var resultType = typeof(TResponse).GetGenericArguments()[0];
            var resultMethod = typeof(UseCaseResult<>).MakeGenericType(resultType).GetMethod(nameof(UseCaseResult<int>.ValidationError));

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.
            return (TResponse)resultMethod.Invoke(null, [validationResult]);
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

        private static TResponse UseCaseResultResponse(ValidationResult validationResult)
        {
            return (TResponse)(object)UseCaseResult.ValidationError(validationResult);
        }
    }
}