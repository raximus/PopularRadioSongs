using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList
{
    public class GetArtistsSongsCountListQueryHandler : IRequestHandler<GetArtistsSongsCountListQuery, UseCaseResult<List<ArtistSongsCountListDto>>>
    {
        private readonly IArtistRepository _artistRepository;

        public GetArtistsSongsCountListQueryHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<UseCaseResult<List<ArtistSongsCountListDto>>> Handle(GetArtistsSongsCountListQuery request, CancellationToken cancellationToken)
        {
            var artists = await _artistRepository.GetArtistSongsCountListAsync();

            return UseCaseResult<List<ArtistSongsCountListDto>>.Success(artists);
        }
    }
}