using Microsoft.EntityFrameworkCore;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Persistence.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly PopularRadioSongsDbContext _dbContext;

        public SongRepository(PopularRadioSongsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SongTitleCountListDto>> GetSongTitleCountListAsync()
        {
            return await _dbContext.Songs.GroupBy(s => s.Lookup).Select(g => new { Title = g.Key, SongsCount = g.Count(), Songs = g.Select(s => new SongTitleCountDto(s.Id, s.Title, s.Artists.Select(a => new ArtistSongTitleCountDto(a.Id, a.Name)).ToList())).ToList() })
                .Where(g => g.SongsCount > 1).OrderByDescending(g => g.SongsCount).Select(g => new SongTitleCountListDto(g.Title, g.SongsCount, g.Songs)).ToListAsync();
        }

        public async Task<Song?> GetSongWithArtistsByIdAsync(int songId)
        {
            return await _dbContext.Songs.Include(s => s.Artists).AsNoTracking().FirstOrDefaultAsync(s => s.Id == songId);
        }
    }
}