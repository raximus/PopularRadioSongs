using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PopularRadioSongs.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHangfire(configuration =>
                configuration.UseInMemoryStorage());

            services.AddHangfireServer();

            services.AddSingleton<BackgroundTasksManager>();

            return services;
        }

        public static IApplicationBuilder StartBackgroundTasks(this IApplicationBuilder app)
        {
            var backgroundTasksManager = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BackgroundTasksManager>();
            backgroundTasksManager?.StartTasks();

            app.UseHangfireDashboard();

            return app;
        }
    }
}