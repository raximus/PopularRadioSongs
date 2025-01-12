using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Mvc.Controllers
{
    public static class UseCaseResultExtension
    {
        public static IActionResult FailureToActionResult(this UseCaseResult useCaseResult) => useCaseResult.Failure switch
        {
            BadRequestFailure => BadRequest(),
            NotFoundFailure => NotFound(),
            ValidationFailure => ValidationProblem(),
            _ => throw new NotSupportedException($"Failure {useCaseResult.Failure} is not supported.")
        };

        private static BadRequestResult BadRequest()
        {
            return new BadRequestResult();
        }

        private static NotFoundResult NotFound()
        {
            return new NotFoundResult();
        }

        private static BadRequestResult ValidationProblem()
        {
            return new BadRequestResult();
        }
    }
}