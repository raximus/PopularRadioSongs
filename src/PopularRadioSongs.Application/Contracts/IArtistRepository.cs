using PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Contracts
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetArtistsAsync();
        Task<List<ArtistSongsCountListDto>> GetArtistsBySongsCountAsync();
        Task<Artist?> GetArtistWithSongsByIdAsync(int artistId);
    }
}