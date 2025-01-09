using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using PopularRadioSongs.Application;
using PopularRadioSongs.Application.Options;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsList;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList;
using PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks;
using PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks;
using PopularRadioSongs.Application.UseCases.Search.GetSearchResults;
using PopularRadioSongs.Application.UseCases.Songs.GetSongDetails;
using PopularRadioSongs.Application.UseCases.Songs.GetSongsList;
using PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList;
using PopularRadioSongs.Infrastructure;
using PopularRadioSongs.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/ping", (ILogger<Program> logger) =>
{
    logger.LogInformation("Ping-pong time {time}", DateTimeOffset.Now);

    return "pong";
});

var apiGroup = app.MapGroup("/api");

apiGroup.MapGet("/artists", async (ISender sender) =>
{
    var artists = await sender.Send(new GetArtistsListQuery());

    return TypedResults.Ok(artists);
});

apiGroup.MapGet("/artists/songscount", async (ISender sender) =>
{
    var artists = await sender.Send(new GetArtistsSongsCountListQuery());

    return TypedResults.Ok(artists);
});

apiGroup.MapGet("/artists/{artistId:int}", async Task<Results<Ok<ArtistDetailsDto>, NotFound>> ([AsParameters] GetArtistDetailsQuery artistDetailsQuery, ISender sender) =>
{
    var artist = await sender.Send(artistDetailsQuery);

    return artist is null ? TypedResults.NotFound() : TypedResults.Ok(artist);
});

apiGroup.MapGet("/songs", async (ISender sender) =>
{
    var songs = await sender.Send(new GetSongsListQuery());

    return TypedResults.Ok(songs);
});

apiGroup.MapGet("/songs/titlecount", async (ISender sender) =>
{
    var songs = await sender.Send(new GetSongsTitleCountListQuery());

    return TypedResults.Ok(songs);
});

apiGroup.MapGet("/songs/{songId:int}", async Task<Results<Ok<SongDetailsDto>, NotFound>> ([AsParameters] GetSongDetailsQuery songDetailsQuery, ISender sender) =>
{
    var song = await sender.Send(songDetailsQuery);

    return song is null ? TypedResults.NotFound() : TypedResults.Ok(song);
});

apiGroup.MapGet("/radioplaybacks/{radioId:int}", async Task<Results<Ok<LastPlaybacksDto>, NotFound>> ([AsParameters] GetLastPlaybacksQuery lastPlaybacksQuery, ISender sender) =>
{
    var lastPlaybacks = await sender.Send(lastPlaybacksQuery);

    return lastPlaybacks is null ? TypedResults.NotFound() : TypedResults.Ok(lastPlaybacks);
});

apiGroup.MapGet("/search", async Task<Results<Ok<SearchResultsDto>, ValidationProblem>> ([AsParameters] GetSearchResultsQuery searchResultsQuery, ISender sender) =>
{
    if (searchResultsQuery.SearchValue.Length < 3)
    {
        var validationProblem = new Dictionary<string, string[]>
        {
            { "SearchValue", new string[] { "Search Value must have at least 3 characters" } }
        };
        return TypedResults.ValidationProblem(validationProblem);
    }

    var searchResults = await sender.Send(searchResultsQuery);

    return TypedResults.Ok(searchResults);
});

apiGroup.MapPost("imports/{hoursRange:int}", async Task<Results<NoContent, ValidationProblem, NotFound>> ([AsParameters] ImportPlaybacksCommand importPlaybacksCommand, ISender sender, IOptions<AppOptions> appOptions, IWebHostEnvironment hostEnvironment) =>
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
});

//app.StartBackgroundTasks();

app.Run();

Log.CloseAndFlush();