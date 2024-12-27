using Microsoft.Extensions.Logging;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Application.Services
{
    public class PlaybacksImporterService : IPlaybacksImporterService
    {
        private readonly ILogger<PlaybacksImporterService> _logger;
        private readonly ISongRepository _songRepository;

        public PlaybacksImporterService(ILogger<PlaybacksImporterService> logger, ISongRepository songRepository)
        {
            _logger = logger;
            _songRepository = songRepository;
        }

        public void ImportPlaybacks()
        {
            _logger.LogInformation("Importing Playbacks");
        }
    }
}