using PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Contracts
{
    public interface IArtistRepository
    {
        Task<(List<Artist>, int)> GetArtistsAsync(int page, int pageSize);
        Task<(List<ArtistSongsCountListDto>, int)> GetArtistSongsCountListAsync(int page, int pageSize);
        Task<Artist?> GetArtistWithSongsByIdAsync(int artistId);
        Task<List<Artist>> GetArtistsBySearchAsync(string searchLookup);
    }
}