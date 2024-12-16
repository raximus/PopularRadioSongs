using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsList
{
    public class GetArtistsListQueryHandler : IRequestHandler<GetArtistsListQuery, List<ArtistListDto>>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public GetArtistsListQueryHandler(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<List<ArtistListDto>> Handle(GetArtistsListQuery request, CancellationToken cancellationToken)
        {
            var artists = await _artistRepository.GetArtistsAsync();

            return _mapper.Map<List<ArtistListDto>>(artists);
        }
    }
}