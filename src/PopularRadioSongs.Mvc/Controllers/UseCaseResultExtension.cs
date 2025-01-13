using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.Results;
using PopularRadioSongs.Mvc.Models;

namespace PopularRadioSongs.Mvc.Controllers
{
    public static class UseCaseResultExtension
    {
        public static PagedViewModel<T> ToPagedViewModel<T>(this PagedUseCaseResult<T> pagedUseCaseResult)
        {
            return new PagedViewModel<T>(new PagedData(pagedUseCaseResult.Page, pagedUseCaseResult.PageSize, pagedUseCaseResult.TotalPages), pagedUseCaseResult.Value);
        }

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