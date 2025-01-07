using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PopularRadioSongs.Application;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsList;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList;
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

//app.StartBackgroundTasks();

app.Run();

Log.CloseAndFlush();