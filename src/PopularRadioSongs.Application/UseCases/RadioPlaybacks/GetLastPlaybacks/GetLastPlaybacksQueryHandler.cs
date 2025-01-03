using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks
{
    public class GetLastPlaybacksQueryHandler : IRequestHandler<GetLastPlaybacksQuery, LastPlaybacksDto?>
    {
        private readonly IPlaybackRepository _playbackRepository;
        private readonly IMapper _mapper;
        private readonly IRadioNamesService _radioNamesService;

        public GetLastPlaybacksQueryHandler(IPlaybackRepository playbackRepository, IMapper mapper, IRadioNamesService radioNamesService)
        {
            _playbackRepository = playbackRepository;
            _mapper = mapper;
            _radioNamesService = radioNamesService;
        }

        public async Task<LastPlaybacksDto?> Handle(GetLastPlaybacksQuery request, CancellationToken cancellationToken)
        {
            if (!_radioNamesService.ConfirmRadioExist(request.RadioId))
            {
                return null;
            }

            var lastPlaybacks = await _playbackRepository.GetLastRadioPlaybacksAsync(request.RadioId);

            var lastPlaybacksDto = _mapper.Map<List<PlaybackLastPlaybacksDto>>(lastPlaybacks);

            var radioName = _radioNamesService.GetRadioName(request.RadioId);

            return new LastPlaybacksDto(radioName, lastPlaybacksDto);
        }
    }
}