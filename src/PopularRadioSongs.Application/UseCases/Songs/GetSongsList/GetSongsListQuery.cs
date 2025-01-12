using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsList
{
    public record GetSongsListQuery() : IRequest<UseCaseResult<List<GroupSongListDto>>>;
}