using MediatR;
using Microsoft.Extensions.Options;
using PopularRadioSongs.Application.Options;
using PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks;

namespace PopularRadioSongs.Api.Endpoints
{
    public static class ImportsEndpoints
    {
        public static void RegisterImportsEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapPost("imports/{hoursRange:int}", ImportPlaybacks)
                .WithName("ImportPlaybacks").WithSummary("Import Playbacks")
                .Produces(StatusCodes.Status204NoContent).ProducesProblem(StatusCodes.Status404NotFound).ProducesValidationProblem();
        }

        static async Task<IResult> ImportPlaybacks([AsParameters] ImportPlaybacksCommand importPlaybacksCommand,
            ISender sender, IOptions<AppOptions> appOptions, IWebHostEnvironment hostEnvironment)
        {
            if (!appOptions.Value.ManualImportOnProduction && !hostEnvironment.IsDevelopment())
            {
                return TypedResults.NotFound();
            }

            var result = await sender.Send(importPlaybacksCommand);

            return result.IsSuccess ? TypedResults.NoContent() : result.FailureToMinimalApi();
        }
    }
}