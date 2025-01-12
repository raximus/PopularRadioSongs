using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList
{
    public record GetSongsTitleCountListQuery() : IRequest<UseCaseResult<List<SongTitleCountListDto>>>;
}