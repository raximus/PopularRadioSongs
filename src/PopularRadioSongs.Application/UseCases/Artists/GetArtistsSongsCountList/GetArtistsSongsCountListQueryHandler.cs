using MediatR;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList
{
    public class GetArtistsSongsCountListQueryHandler : IRequestHandler<GetArtistsSongsCountListQuery, List<ArtistSongsCountListDto>>
    {
        private readonly IArtistRepository _artistRepository;

        public GetArtistsSongsCountListQueryHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<List<ArtistSongsCountListDto>> Handle(GetArtistsSongsCountListQuery request, CancellationToken cancellationToken)
        {
            var artists = await _artistRepository.GetArtistSongsCountListAsync();

            return artists;
        }
    }
}