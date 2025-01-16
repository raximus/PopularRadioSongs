using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Infrastructure.RadioStations;
using System.Net;

namespace PopularRadioSongs.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHangfire(configuration => configuration.UseInMemoryStorage()
                .UseFilter(new AutomaticRetryAttribute() { Attempts = 3, DelaysInSeconds = [300] }));

            services.AddHangfireServer();

            services.AddSingleton<BackgroundTasksManager>();

            services.AddHttpClient(Options.DefaultName).ConfigureHttpClient(options => options.DefaultRequestHeaders.Accept.ParseAdd("application/json"))
                .ConfigurePrimaryHttpMessageHandler(h => new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });

            services.AddSingleton<IRadioStation, RmfFmRadioStation>();
            services.AddSingleton<IRadioStation, ZetRadioStation>();
            services.AddSingleton<IRadioStation, EskaRadioStation>();

            return services;
        }

        public static IApplicationBuilder StartBackgroundTasks(this IApplicationBuilder app)
        {
            var backgroundTasksManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<BackgroundTasksManager>();
            backgroundTasksManager.StartTasks();

            app.UseHangfireDashboard();

            return app;
        }
    }
}