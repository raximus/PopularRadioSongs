using Microsoft.EntityFrameworkCore;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Persistence.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly PopularRadioSongsDbContext _dbContext;

        public ArtistRepository(PopularRadioSongsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<Artist>, int)> GetArtistsAsync(int page, int pageSize)
        {
            var query = _dbContext.Artists.AsNoTracking().OrderBy(a => a.Name);

            var artistsCount = await query.CountAsync();
            var artists = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return (artists, artistsCount);
        }

        public async Task<(List<ArtistSongsCountListDto>, int)> GetArtistSongsCountListAsync(int page, int pageSize)
        {
            var query = _dbContext.Artists.Where(a => a.Songs.Count > 1).OrderByDescending(a => a.Songs.Count);

            var artistsCount = await query.CountAsync();
            var artists = await query.Skip((page - 1) * pageSize).Take(pageSize).Select(a => new ArtistSongsCountListDto(a.Id, a.Name, a.Songs.Count)).ToListAsync();

            return (artists, artistsCount);
        }

        public async Task<Artist?> GetArtistWithSongsByIdAsync(int artistId)
        {
            return await _dbContext.Artists.Include(a => a.Songs).ThenInclude(s => s.Artists).FirstOrDefaultAsync(a => a.Id == artistId);
        }

        public async Task<List<Artist>> GetArtistsBySearchAsync(string searchLookup)
        {
            return await _dbContext.Artists.AsNoTracking().Where(a => a.Lookup.Contains(searchLookup)).ToListAsync();
        }
    }
}