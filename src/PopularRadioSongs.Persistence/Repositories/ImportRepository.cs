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

        public async Task<Song?> GetSongByLookupsAsync(string songLookup, List<string> artistsLookups)
        {
            return await _dbContext.Songs.Include(s => s.Artists).Where(s => s.Lookup == songLookup && s.Artists.Any(a => artistsLookups.Contains(a.Lookup))).FirstOrDefaultAsync();
        }

        public async Task AddArtistAsync(Artist artist)
        {
            await _dbContext.Artists.AddAsync(artist);
        }

        public async Task AddSongAsync(Song song)
        {
            await _dbContext.Songs.AddAsync(song);
        }

        public async Task AddPlaybackAsync(Playback playback)
        {
            await _dbContext.Playbacks.AddAsync(playback);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}