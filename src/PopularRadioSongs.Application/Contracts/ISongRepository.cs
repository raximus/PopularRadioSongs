using PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Contracts
{
    public interface ISongRepository
    {
        Task<(List<Song>, int)> GetSongsAsync(int page, int pageSize);
        Task<(List<SongTitleCountListDto>, int)> GetSongTitleCountListAsync(int page, int pageSize);
        Task<Song?> GetSongWithArtistsAndPlaybacksByIdAsync(int songId);
        Task<List<Song>> GetSongsBySearchAsync(string searchLookup);
    }
}