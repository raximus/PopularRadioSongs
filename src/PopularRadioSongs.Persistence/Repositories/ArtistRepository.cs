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

        public async Task<List<Artist>> GetArtistsAsync()
        {
            return await _dbContext.Artists.AsNoTracking().OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<List<ArtistSongsCountListDto>> GetArtistSongsCountListAsync()
        {
            return await _dbContext.Artists.Select(a => new { a.Id, a.Name, SongsCount = a.Songs.Count }).Where(a => a.SongsCount > 1).OrderByDescending(a => a.SongsCount)
                .Select(a => new ArtistSongsCountListDto(a.Id, a.Name, a.SongsCount)).ToListAsync();
        }

        public async Task<Artist?> GetArtistWithSongsByIdAsync(int artistId)
        {
            return await _dbContext.Artists.Include(a => a.Songs).ThenInclude(s => s.Artists).FirstOrDefaultAsync(a => a.Id == artistId);
        }
    }
}