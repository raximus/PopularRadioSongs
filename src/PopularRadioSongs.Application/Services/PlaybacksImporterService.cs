using Microsoft.Extensions.Logging;
using PopularRadioSongs.Application.Contracts;
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
            var now = DateTimeOffset.Now;
            var playbacksTime = new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, 0, 0, now.Offset).AddHours(-hoursRange);

            _logger.LogInformation("Start importing Playbacks for {playbacksTime} time", playbacksTime);

            foreach (var radioStation in _radioStations)
            {
                var playbacks = await radioStation.GetPlaybacksAsync(playbacksTime);

                var newArtistsCount = 0;
                var newSongsCount = 0;
                var updatedSongsArtistsCount = 0;
                foreach (var playbackDraft in playbacks)
                {
                    var song = await _importRepository.GetSongByLookupsAsync(playbackDraft.SongLookup, playbackDraft.Artists.Select(a => a.Lookup).ToList());

                    if (song == null)
                    {
                        var artists = new List<Artist>(playbackDraft.Artists.Count);
                        foreach (var artistDraft in playbackDraft.Artists)
                        {
                            var artist = await _importRepository.GetArtistByLookupAsync(artistDraft.Lookup);

                            if (artist == null)
                            {
                                artist = new Artist(artistDraft.Name);
                                await _importRepository.AddArtistAsync(artist);
                                newArtistsCount++;
                            }

                            artists.Add(artist);
                        }

                        song = new Song(playbackDraft.SongTitle, artists);
                        await _importRepository.AddSongAsync(song);
                        newSongsCount++;
                    }
                    else
                    {
                        foreach (var artistDraft in playbackDraft.Artists)
                        {
                            if (!song.Artists.Any(a => a.Lookup == artistDraft.Lookup))
                            {
                                var artist = await _importRepository.GetArtistByLookupAsync(artistDraft.Lookup);

                                if (artist == null)
                                {
                                    artist = new Artist(artistDraft.Name);
                                    await _importRepository.AddArtistAsync(artist);
                                    newArtistsCount++;
                                }

                                song.AddArtist(artist);
                                updatedSongsArtistsCount++;
                            }
                        }
                    }

                    var playback = new Playback(song, radioStation.Id, playbackDraft.PlayTime);
                    await _importRepository.AddPlaybackAsync(playback);

                    await _importRepository.SaveAsync();
                }

                _logger.LogInformation("Imported {playbacksCount} Playbacks, including {newSongsCount} new songs, {newArtistsCount} new artists, and {updatedSongsArtistsCount} updated song artists", playbacks.Count, newSongsCount, newArtistsCount, updatedSongsArtistsCount);
            }
        }
    }
}