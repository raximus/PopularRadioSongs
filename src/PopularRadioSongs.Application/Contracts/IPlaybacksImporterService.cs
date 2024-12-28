namespace PopularRadioSongs.Application.Contracts
{
    public interface IPlaybacksImporterService
    {
        Task ImportPlaybacksAsync(int hoursRange);
    }
}