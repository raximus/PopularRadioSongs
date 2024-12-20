using PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Contracts
{
    public interface ISongRepository
    {
        Task<List<SongTitleCountListDto>> GetSongTitleCountListAsync();
        Task<Song?> GetSongWithArtistsByIdAsync(int songId);
    }
}