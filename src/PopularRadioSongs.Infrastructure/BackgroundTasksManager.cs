using Hangfire;
using Microsoft.Extensions.Options;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Options;

namespace PopularRadioSongs.Infrastructure
{
    public class BackgroundTasksManager
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly AppOptions _appOptions;

        public BackgroundTasksManager(IRecurringJobManager recurringJobManager, IOptions<AppOptions> appOptions)
        {
            _recurringJobManager = recurringJobManager;
            _appOptions = appOptions.Value;
        }

        public void StartTasks()
        {
            _recurringJobManager.AddOrUpdate<IPlaybacksImporterService>("PlaybacksImporterJob", p => p.ImportPlaybacksAsync(1), Cron.Hourly(_appOptions.PlaybacksImporterStartMinute));
        }
    }
}