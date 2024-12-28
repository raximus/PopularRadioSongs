using Hangfire;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Infrastructure
{
    public class BackgroundTasksManager
    {
        private readonly IRecurringJobManager _recurringJobManager;

        public BackgroundTasksManager(IRecurringJobManager recurringJobManager)
        {
            _recurringJobManager = recurringJobManager;
        }

        public void StartTasks()
        {
            _recurringJobManager.AddOrUpdate<IPlaybacksImporterService>("PlaybacksImporterJob", p => p.ImportPlaybacksAsync(), Cron.Minutely);
        }
    }
}