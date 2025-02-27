﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PopularRadioSongs.Application.UseCases.Songs.GetSongDetails;
using PopularRadioSongs.Application.UseCases.Songs.GetSongsList;
using PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList;

namespace PopularRadioSongs.Api.Endpoints
{
    public static class SongsEndpoints
    {
        public static void RegisterSongsEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapGet("/songs", GetSongsList)
                .WithName("GetSongsList").WithSummary("Get Songs List");

            builder.MapGet("/songs/titlecount", GetSongsTitleCountList)
                .WithName("GetSongsTitleCountList").WithSummary("Get Songs TitleCount List");

            builder.MapGet("/songs/{songId:int}", GetSongDetails)
                .WithName("GetSongDetails").WithSummary("Get Song Details")
                .Produces<SongDetailsDto>().ProducesProblem(StatusCodes.Status404NotFound);
        }

        static async Task<Ok<List<GroupSongListDto>>> GetSongsList([AsParameters] GetSongsListQuery songsListQuery, ISender sender, HttpResponse response)
        {
            var songs = await sender.Send(songsListQuery);

            response.AddPaginationHeader(songs);

            return TypedResults.Ok(songs.Value);
        }

        static async Task<Ok<List<SongTitleCountListDto>>> GetSongsTitleCountList([AsParameters] GetSongsTitleCountListQuery songsTitleCountListQuery, ISender sender, HttpResponse response)
        {
            var songs = await sender.Send(songsTitleCountListQuery);

            response.AddPaginationHeader(songs);

            return TypedResults.Ok(songs.Value);
        }

        static async Task<IResult> GetSongDetails([AsParameters] GetSongDetailsQuery songDetailsQuery, ISender sender)
        {
            var song = await sender.Send(songDetailsQuery);

            return song.IsSuccess ? TypedResults.Ok(song.Value) : song.FailureToMinimalApi();
        }
    }
}