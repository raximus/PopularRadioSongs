using MediatR;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks
{
    public class ImportPlaybacksCommandHandler : IRequestHandler<ImportPlaybacksCommand>
    {
        private readonly IPlaybacksImporterService _playbacksImporterService;

        public ImportPlaybacksCommandHandler(IPlaybacksImporterService playbacksImporterService)
        {
            _playbacksImporterService = playbacksImporterService;
        }

        public async Task Handle(ImportPlaybacksCommand request, CancellationToken cancellationToken)
        {
            for (int i = request.HoursRange; i > 0; i--)
            {
                await _playbacksImporterService.ImportPlaybacksAsync(i);
            }
        }
    }
}