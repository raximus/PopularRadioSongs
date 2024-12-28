using Microsoft.Extensions.Logging;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Application.Services
{
    public class PlaybacksImporterService : IPlaybacksImporterService
    {
        private readonly ILogger<PlaybacksImporterService> _logger;
        private readonly ISongRepository _songRepository;
        private readonly IEnumerable<IRadioStation> _radioStations;

        public PlaybacksImporterService(ILogger<PlaybacksImporterService> logger, ISongRepository songRepository, IEnumerable<IRadioStation> radioStations)
        {
            _logger = logger;
            _songRepository = songRepository;
            _radioStations = radioStations;
        }

        public async Task ImportPlaybacksAsync(int hoursRange)
        {
            _logger.LogInformation("Importing Playbacks for {0} hours", hoursRange);

            var playbacksTime = new DateTimeOffset(2024, 12, 28, 13, 0, 0, TimeSpan.FromHours(1));

            foreach (var radioStation in _radioStations)
            {
                var playbacks = await radioStation.GetPlaybacksAsync(playbacksTime);

                _logger.LogInformation("Pobrano {0} odtworzeń dla Radia {1}(Id {2})", playbacks.Count, radioStation.Name, radioStation.Id);
            }
        }
    }
}