using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsList
{
    public record GetSongsListQuery(int Page = 1, int PageSize = 80) : PagedQuery(Page, PageSize, 100), IRequest<PagedUseCaseResult<List<GroupSongListDto>>>;
}