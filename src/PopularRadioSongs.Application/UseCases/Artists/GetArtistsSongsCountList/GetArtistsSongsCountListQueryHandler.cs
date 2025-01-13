using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList
{
    public class GetArtistsSongsCountListQueryHandler : IRequestHandler<GetArtistsSongsCountListQuery, PagedUseCaseResult<List<ArtistSongsCountListDto>>>
    {
        private readonly IArtistRepository _artistRepository;

        public GetArtistsSongsCountListQueryHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<PagedUseCaseResult<List<ArtistSongsCountListDto>>> Handle(GetArtistsSongsCountListQuery request, CancellationToken cancellationToken)
        {
            (var artists, var artistsCount) = await _artistRepository.GetArtistSongsCountListAsync(request.Page, request.PageSize);

            return PagedUseCaseResult<List<ArtistSongsCountListDto>>.Success(request.Page, request.PageSize, artistsCount, artists);
        }
    }
}