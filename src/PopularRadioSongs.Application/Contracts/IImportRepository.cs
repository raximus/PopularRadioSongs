using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Contracts
{
    public interface IImportRepository
    {
        Task<Artist?> GetArtistByLookupAsync(string lookup);
        Task<Song?> GetSongByLookupAndArtistsAsync(string songLookup, List<Artist> artists);
        Task AddAndSaveAsync(Playback playback);
    }
}