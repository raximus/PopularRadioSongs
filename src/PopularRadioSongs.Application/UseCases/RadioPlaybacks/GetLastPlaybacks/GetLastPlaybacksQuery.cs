using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks
{
    public record GetLastPlaybacksQuery(int RadioId, int Page = 1, int PageSize = 200) : PagedQuery(Page, PageSize, 300), IRequest<PagedUseCaseResult<LastPlaybacksDto>>;
}