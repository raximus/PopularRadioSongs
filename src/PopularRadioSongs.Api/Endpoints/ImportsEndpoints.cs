using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using PopularRadioSongs.Application.Options;
using PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks;

namespace PopularRadioSongs.Api.Endpoints
{
    public static class ImportsEndpoints
    {
        public static void RegisterImportsEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapPost("imports/{hoursRange:int}", ImportPlaybacks).WithName("ImportPlaybacks").WithSummary("Import Playbacks");
        }

        static async Task<Results<NoContent, ValidationProblem, NotFound>> ImportPlaybacks([AsParameters] ImportPlaybacksCommand importPlaybacksCommand,
            ISender sender, IOptions<AppOptions> appOptions, IWebHostEnvironment hostEnvironment)
        {
            if (!appOptions.Value.ManualImportOnProduction && !hostEnvironment.IsDevelopment())
            {
                return TypedResults.NotFound();
            }

            if (importPlaybacksCommand.HoursRange < 1 || importPlaybacksCommand.HoursRange > 24)
            {
                var validationProblem = new Dictionary<string, string[]>
                    {
                        { "HoursRange", new string[] { "Hours must be between 1 and 24" } }
                    };
                return TypedResults.ValidationProblem(validationProblem);
            }

            await sender.Send(importPlaybacksCommand);

            return TypedResults.NoContent();
        }
    }
}