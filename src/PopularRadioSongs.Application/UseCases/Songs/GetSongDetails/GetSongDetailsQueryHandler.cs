using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongDetails
{
    public class GetSongDetailsQueryHandler : IRequestHandler<GetSongDetailsQuery, SongDetailsDto?>
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public GetSongDetailsQueryHandler(ISongRepository songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task<SongDetailsDto?> Handle(GetSongDetailsQuery request, CancellationToken cancellationToken)
        {
            var song = await _songRepository.GetSongWithArtistsByIdAsync(request.SongId);

            if (song == null)
            {
                return null;
            }

            return _mapper.Map<SongDetailsDto>(song);
        }
    }
}