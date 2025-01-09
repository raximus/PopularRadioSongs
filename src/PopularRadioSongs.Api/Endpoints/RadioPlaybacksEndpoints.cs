using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks;

namespace PopularRadioSongs.Api.Endpoints
{
    public static class RadioPlaybacksEndpoints
    {
        public static void RegisterRadioPlaybacksEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapGet("/radioplaybacks/{radioId:int}", GetLastPlaybacks).WithName("GetLastPlaybacks").WithSummary("Get Last Playbacks");
        }

        static async Task<Results<Ok<LastPlaybacksDto>, NotFound>> GetLastPlaybacks([AsParameters] GetLastPlaybacksQuery lastPlaybacksQuery, ISender sender)
        {
            var lastPlaybacks = await sender.Send(lastPlaybacksQuery);

            return lastPlaybacks is null ? TypedResults.NotFound() : TypedResults.Ok(lastPlaybacks);
        }
    }
}