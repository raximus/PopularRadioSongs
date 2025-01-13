using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList
{
    public record GetArtistsSongsCountListQuery(int Page = 1, int PageSize = 50) : PagedQuery(Page, PageSize, 100), IRequest<PagedUseCaseResult<List<ArtistSongsCountListDto>>>;
}