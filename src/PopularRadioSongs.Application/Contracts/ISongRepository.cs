using PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Contracts
{
    public interface ISongRepository
    {
        Task<List<Song>> GetSongsAsync();
        Task<List<SongTitleCountListDto>> GetSongTitleCountListAsync();
        Task<Song?> GetSongWithArtistsAndPlaybacksByIdAsync(int songId);
        Task<List<Song>> GetSongsBySearchAsync(string searchLookup);
    }
}