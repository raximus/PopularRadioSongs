using PopularRadioSongs.Core.Common;

namespace PopularRadioSongs.Application.Contracts
{
    public interface IRadioStation
    {
        int Id { get; }
        string Name { get; }

        Task<List<PlaybackDraft>> GetPlaybacksAsync(DateTimeOffset playbacksTime);
    }
}