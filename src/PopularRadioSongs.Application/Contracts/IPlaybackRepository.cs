using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Contracts
{
    public interface IPlaybackRepository
    {
        Task<List<Playback>> GetLastRadioPlaybacksAsync(int radioId);
    }
}