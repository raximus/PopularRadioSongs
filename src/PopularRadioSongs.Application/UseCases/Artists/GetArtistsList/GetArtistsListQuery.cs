using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsList
{
    public record GetArtistsListQuery(int Page = 1, int PageSize = 50) : PagedQuery(Page, PageSize, 100), IRequest<PagedUseCaseResult<List<GroupArtistListDto>>>;
}