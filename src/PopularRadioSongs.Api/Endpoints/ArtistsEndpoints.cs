using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsList;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList;

namespace PopularRadioSongs.Api.Endpoints
{
    public static class ArtistsEndpoints
    {
        public static void RegisterArtistsEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapGet("/artists", GetArtistsList)
                .WithName("GetArtistsList").WithSummary("Get Artists List");

            builder.MapGet("/artists/songscount", GetArtistsSongsCountList)
                .WithName("GetArtistsSongsCountList").WithSummary("Get Artists SongsCount List");

            builder.MapGet("/artists/{artistId:int}", GetArtistDetails)
                .WithName("GetArtistDetails").WithSummary("Get Artist Details")
                .Produces<ArtistDetailsDto>().ProducesProblem(StatusCodes.Status404NotFound);
        }

        static async Task<Ok<List<GroupArtistListDto>>> GetArtistsList(ISender sender)
        {
            var artists = await sender.Send(new GetArtistsListQuery());

            return TypedResults.Ok(artists.Value);
        }

        static async Task<Ok<List<ArtistSongsCountListDto>>> GetArtistsSongsCountList(ISender sender)
        {
            var artists = await sender.Send(new GetArtistsSongsCountListQuery());

            return TypedResults.Ok(artists.Value);
        }

        static async Task<IResult> GetArtistDetails([AsParameters] GetArtistDetailsQuery artistDetailsQuery, ISender sender)
        {
            var artist = await sender.Send(artistDetailsQuery);

            return artist.IsSuccess ? TypedResults.Ok(artist.Value) : artist.FailureToMinimalApi();
        }
    }
}