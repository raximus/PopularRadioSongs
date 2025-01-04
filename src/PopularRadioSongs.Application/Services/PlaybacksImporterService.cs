using Microsoft.Extensions.Logging;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Extensions;
using PopularRadioSongs.Core.Common;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Services
{
    public class PlaybacksImporterService : IPlaybacksImporterService
    {
        private readonly IEnumerable<IRadioStation> _radioStations;
        private readonly IImportRepository _importRepository;
        private readonly ILogger<PlaybacksImporterService> _logger;

        public PlaybacksImporterService(IEnumerable<IRadioStation> radioStations, IImportRepository importRepository, ILogger<PlaybacksImporterService> logger)
        {
            _radioStations = radioStations;
            _importRepository = importRepository;
            _logger = logger;
        }

        public async Task ImportPlaybacksAsync(int hoursRange)
        {
            var playbacksTime = DateTimeOffset.Now.ToLastFullHour().AddHours(-hoursRange);

            _logger.LogInformation("Start importing Playbacks for {playbacksTime} time", playbacksTime);

            foreach (var radioStation in _radioStations)
            {
                var playbacks = await radioStation.GetPlaybacksAsync(playbacksTime);

                var newArtistsCount = 0;
                var newSongsCount = 0;
                var appendedSongArtistsCount = 0;
                foreach (var playbackDraft in playbacks)
                {
                    (var artists, newArtistsCount) = await GetArtistsAsync(playbackDraft, newArtistsCount);

                    var song = await _importRepository.GetSongByLookupAndArtistsAsync(playbackDraft.SongLookup, artists);

                    if (song == null)
                    {
                        song = new Song(playbackDraft.SongTitle, artists);
                        newSongsCount++;
                    }
                    else
                    {
                        appendedSongArtistsCount += song.AppendArtists(artists);
                    }

                    var playback = new Playback(song, radioStation.Id, playbackDraft.PlayTime);

                    await _importRepository.AddAndSaveAsync(playback);
                }

                _logger.LogInformation("Imported {playbacksCount} Playbacks, including {newSongsCount} new songs, {newArtistsCount} new artists, and {appendedSongArtistsCount} appended song artists", playbacks.Count, newSongsCount, newArtistsCount, appendedSongArtistsCount);
            }
        }

        private async Task<(List<Artist> artists, int newArtistsCount)> GetArtistsAsync(PlaybackDraft playbackDraft, int newArtistsCount)
        {
            var artists = new List<Artist>(playbackDraft.Artists.Count);
            foreach (var artistDraft in playbackDraft.Artists)
            {
                var artist = await _importRepository.GetArtistByLookupAsync(artistDraft.Lookup);

                if (artist == null)
                {
                    artist = new Artist(artistDraft.Name);
                    newArtistsCount++;
                }

                artists.Add(artist);
            }

            return (artists, newArtistsCount);
        }
    }
}