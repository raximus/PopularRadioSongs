using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Contracts
{
    public interface IPlaybackRepository
    {
        Task<(List<Playback>, int)> GetLastRadioPlaybacksAsync(int radioId, int page, int pageSize);
    }
}