using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList
{
    public record GetSongsTitleCountListQuery(int Page = 1, int PageSize = 20) : PagedQuery(Page, PageSize, 50), IRequest<PagedUseCaseResult<List<SongTitleCountListDto>>>;
}