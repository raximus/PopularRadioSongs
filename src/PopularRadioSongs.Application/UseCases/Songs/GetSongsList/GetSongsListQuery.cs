using MediatR;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsList
{
    public record GetSongsListQuery() : IRequest<List<GroupSongListDto>>;
}