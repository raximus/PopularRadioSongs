using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongDetails
{
    public class GetSongDetailsQueryHandler : IRequestHandler<GetSongDetailsQuery, SongDetailsDto?>
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

        public async Task<SongDetailsDto?> Handle(GetSongDetailsQuery request, CancellationToken cancellationToken)
        {
            var song = await _songRepository.GetSongWithArtistsAndPlaybacksByIdAsync(request.SongId);

            if (song == null)
            {
                return null;
            }

            var songDto = _mapper.Map<SongDetailsDto>(song);

            songDto.Playbacks.ForEach(playback =>
            {
                playback.RadioName = _radioNamesService.GetRadioName(playback.RadioId);
            });

            return songDto;
        }
    }
}