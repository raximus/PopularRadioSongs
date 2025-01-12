using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongDetails
{
    public class GetSongDetailsQueryHandler : IRequestHandler<GetSongDetailsQuery, UseCaseResult<SongDetailsDto>>
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;
        private readonly IRadioNamesService _radioNamesService;

        public GetSongDetailsQueryHandler(ISongRepository songRepository, IMapper mapper, IRadioNamesService radioNamesService)
        {
            _songRepository = songRepository;
            _mapper = mapper;
            _radioNamesService = radioNamesService;
        }

        public async Task<UseCaseResult<SongDetailsDto>> Handle(GetSongDetailsQuery request, CancellationToken cancellationToken)
        {
            var song = await _songRepository.GetSongWithArtistsAndPlaybacksByIdAsync(request.SongId);

            if (song is null)
            {
                return UseCaseResult<SongDetailsDto>.NotFound("Song not found");
            }

            var songDto = _mapper.Map<SongDetailsDto>(song);

            songDto.Playbacks.ForEach(playback =>
            {
                playback.RadioName = _radioNamesService.GetRadioName(playback.RadioId);
            });

            return UseCaseResult<SongDetailsDto>.Success(songDto);
        }
    }
}