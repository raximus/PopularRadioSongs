using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Extensions;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks
{
    public class GetLastPlaybacksQueryHandler : IRequestHandler<GetLastPlaybacksQuery, PagedUseCaseResult<LastPlaybacksDto>>
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

        public async Task<PagedUseCaseResult<LastPlaybacksDto>> Handle(GetLastPlaybacksQuery request, CancellationToken cancellationToken)
        {
            if (!_radioNamesService.ConfirmRadioExist(request.RadioId))
            {
                return PagedUseCaseResult<LastPlaybacksDto>.NotFound("Radio not found");
            }

            (var lastPlaybacks, var lastPlaybacksCount) = await _playbackRepository.GetLastRadioPlaybacksAsync(request.RadioId, request.Page, request.PageSize);

            var lastPlaybacksDto = _mapper.Map<List<PlaybackLastPlaybacksDto>>(lastPlaybacks);

            var lastPlaybackGroups = lastPlaybacksDto.GroupBy(p => p.PlayTime.ToLastFullHour()).Select(g => new PlaybackGroupLastPlaybacksDto(g.Key, g.Key.AddHours(1), g.ToList())).ToList();

            var radioName = _radioNamesService.GetRadioName(request.RadioId);

            return PagedUseCaseResult<LastPlaybacksDto>.Success(request.Page, request.PageSize, lastPlaybacksCount, new LastPlaybacksDto(radioName, lastPlaybackGroups));
        }
    }
}