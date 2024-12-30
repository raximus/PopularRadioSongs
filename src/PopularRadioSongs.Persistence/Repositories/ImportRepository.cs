using Microsoft.EntityFrameworkCore;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Persistence.Repositories
{
    public class ImportRepository : IImportRepository
    {
        private readonly PopularRadioSongsDbContext _dbContext;

        public ImportRepository(PopularRadioSongsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Artist?> GetArtistByLookupAsync(string lookup)
        {
            return await _dbContext.Artists.FirstOrDefaultAsync(a => a.Lookup == lookup);
        }

        public async Task<Song?> GetSongByLookupAndArtistsAsync(string songLookup, List<Artist> artists)
        {
            return await _dbContext.Songs.Include(s => s.Artists).FirstOrDefaultAsync(s => s.Lookup == songLookup && s.Artists.Any(a => artists.Contains(a)));
        }

        public async Task AddAndSaveAsync(Playback playback)
        {
            await _dbContext.Playbacks.AddAsync(playback);

            await _dbContext.SaveChangesAsync();
        }
    }
}