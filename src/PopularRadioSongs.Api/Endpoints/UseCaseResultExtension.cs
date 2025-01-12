using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Api.Endpoints
{
    public static class UseCaseResultExtension
    {
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