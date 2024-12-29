using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Contracts
{
    public interface IImportRepository
    {
        Task<Artist?> GetArtistByLookupAsync(string lookup);
        Task<Song?> GetSongByLookupsAsync(string songLookup, List<string> artistsLookups);
        Task AddArtistAsync(Artist artist);
        Task AddSongAsync(Song song);
        Task AddPlaybackAsync(Playback playback);
        Task SaveAsync();
    }
}