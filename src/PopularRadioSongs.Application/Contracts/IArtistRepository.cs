using PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Contracts
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetArtistsAsync();
        Task<List<ArtistSongsCountListDto>> GetArtistSongsCountListAsync();
        Task<Artist?> GetArtistWithSongsByIdAsync(int artistId);
    }
}