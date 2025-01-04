using Microsoft.EntityFrameworkCore;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Persistence.Repositories
{
    public class PlaybackRepository : IPlaybackRepository
    {
        private readonly PopularRadioSongsDbContext _dbContext;

        public PlaybackRepository(PopularRadioSongsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Playback>> GetLastRadioPlaybacksAsync(int radioId)
        {
            return await _dbContext.Playbacks.AsNoTracking().Include(p => p.Song).ThenInclude(s => s.Artists).Where(p => p.RadioId == radioId)
                .OrderByDescending(p => p.PlayTime).ToListAsync();
        }
    }
}