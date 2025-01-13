using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.Results;
using System.Text.Json;

namespace PopularRadioSongs.Api.Endpoints
{
    public static class UseCaseResultExtension
    {
        public static void AddPaginationHeader<T>(this HttpResponse response, PagedUseCaseResult<T> pagedUseCaseResult)
        {
            if (pagedUseCaseResult.IsSuccess)
            {
                response.Headers.Append("X-Pagination", JsonSerializer.Serialize(new
                {
                    pagedUseCaseResult.Page,
                    pagedUseCaseResult.PageSize,
                    pagedUseCaseResult.TotalPages,
                    pagedUseCaseResult.TotalItems
                }));
            }
        }

        public static IResult FailureToMinimalApi(this UseCaseResult useCaseResult) => useCaseResult.Failure switch
        {
            BadRequestFailure => BadRequest((BadRequestFailure)useCaseResult.Failure),
            NotFoundFailure => NotFound((NotFoundFailure)useCaseResult.Failure),
            ValidationFailure => ValidationProblem((ValidationFailure)useCaseResult.Failure),
            _ => throw new NotSupportedException($"Failure {useCaseResult.Failure} is not supported.")
        };

        private static ProblemHttpResult BadRequest(BadRequestFailure failure)
        {
            return TypedResults.Problem(new ProblemDetails() { Status = StatusCodes.Status400BadRequest, Detail = failure.Message });
        }

        private static ProblemHttpResult NotFound(NotFoundFailure failure)
        {
            return TypedResults.Problem(new ProblemDetails() { Status = StatusCodes.Status404NotFound, Detail = failure.Message });
        }

        private static ValidationProblem ValidationProblem(ValidationFailure failure)
        {
            return TypedResults.ValidationProblem(failure.ValidationErrors);
        }
    }
}