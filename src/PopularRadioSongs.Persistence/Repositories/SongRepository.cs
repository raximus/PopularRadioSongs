﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Song>> GetSongsAsync()
        {
            return await _dbContext.Songs.AsNoTracking().Include(s => s.Artists).OrderBy(s => s.Title).ToListAsync();
        }

        public async Task<List<SongTitleCountListDto>> GetSongTitleCountListAsync()
        {
            return await _dbContext.Songs.GroupBy(s => s.Lookup).Where(g => g.Count() > 1).OrderByDescending(g => g.Count())
                .Select(g => new SongTitleCountListDto(g.Key, g.Count(), g
                    .Select(s => new SongTitleCountDto(s.Id, s.Title, s.Artists
                        .Select(a => new ArtistSongTitleCountDto(a.Id, a.Name)).ToList())).ToList()))
                .ToListAsync();
        }

        public async Task<Song?> GetSongWithArtistsAndPlaybacksByIdAsync(int songId)
        {
            return await _dbContext.Songs.AsNoTracking().Include(s => s.Artists).Include(s => s.Playbacks.OrderByDescending(p => p.PlayTime)).FirstOrDefaultAsync(s => s.Id == songId);
        }

        public async Task<List<Song>> GetSongsBySearchAsync(string searchLookup)
        {
            return await _dbContext.Songs.AsNoTracking().Include(s => s.Artists).Where(s => s.Lookup.Contains(searchLookup)).ToListAsync();
        }
    }
}