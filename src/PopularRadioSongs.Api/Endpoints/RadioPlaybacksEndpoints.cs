using MediatR;
using PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks;

namespace PopularRadioSongs.Api.Endpoints
{
    public static class RadioPlaybacksEndpoints
    {
        public static void RegisterRadioPlaybacksEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapGet("/radioplaybacks/{radioId:int}", GetLastPlaybacks)
                .WithName("GetLastPlaybacks").WithSummary("Get Last Playbacks")
                .Produces<LastPlaybacksDto>().ProducesProblem(StatusCodes.Status404NotFound);
        }

        static async Task<IResult> GetLastPlaybacks([AsParameters] GetLastPlaybacksQuery lastPlaybacksQuery, ISender sender)
        {
            var lastPlaybacks = await sender.Send(lastPlaybacksQuery);

            return lastPlaybacks.IsSuccess ? TypedResults.Ok(lastPlaybacks.Value) : lastPlaybacks.FailureToMinimalApi();
        }
    }
}