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

        public async Task<(List<Playback>, int)> GetLastRadioPlaybacksAsync(int radioId, int page, int pageSize)
        {
            var query = _dbContext.Playbacks.AsNoTracking().Include(p => p.Song).ThenInclude(s => s.Artists).Where(p => p.RadioId == radioId)
                .OrderByDescending(p => p.PlayTime);

            var playbacksCount = await query.CountAsync();
            var playbacks = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return (playbacks, playbacksCount);
        }
    }
}