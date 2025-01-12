using PopularRadioSongs.Api.Endpoints;
using PopularRadioSongs.Application;
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

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = context.HttpContext.Request.Path;
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapGet("/ping", (ILogger<Program> logger) =>
{
    logger.LogInformation("Ping-pong time {time}", DateTimeOffset.Now);

    return "pong";
});

var apiGroup = app.MapGroup("/api");

apiGroup.RegisterArtistsEndpoints();

apiGroup.RegisterSongsEndpoints();

apiGroup.RegisterRadioPlaybacksEndpoints();

apiGroup.RegisterSearchEndpoints();

apiGroup.RegisterImportsEndpoints();

//app.StartBackgroundTasks();

app.Run();

Log.CloseAndFlush();