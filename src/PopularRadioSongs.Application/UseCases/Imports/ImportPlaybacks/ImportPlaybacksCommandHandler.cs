using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks
{
    public class ImportPlaybacksCommandHandler : IRequestHandler<ImportPlaybacksCommand, UseCaseResult>
    {
        private readonly IPlaybacksImporterService _playbacksImporterService;

        public ImportPlaybacksCommandHandler(IPlaybacksImporterService playbacksImporterService)
        {
            _playbacksImporterService = playbacksImporterService;
        }

        public async Task<UseCaseResult> Handle(ImportPlaybacksCommand request, CancellationToken cancellationToken)
        {
            for (int i = request.HoursRange; i > 0; i--)
            {
                await _playbacksImporterService.ImportPlaybacksAsync(i);
            }

            return UseCaseResult.Success();
        }
    }
}