using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails
{
    public class GetArtistDetailsQueryHandler : IRequestHandler<GetArtistDetailsQuery, ArtistDetailsDto?>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public GetArtistDetailsQueryHandler(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<ArtistDetailsDto?> Handle(GetArtistDetailsQuery request, CancellationToken cancellationToken)
        {
            var artist = await _artistRepository.GetArtistWithSongsByIdAsync(request.ArtistId);

            if (artist is null)
            {
                return null;
            }

            return _mapper.Map<ArtistDetailsDto>(artist);
        }
    }
}