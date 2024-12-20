using MediatR;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList
{
    public record GetSongsTitleCountListQuery() : IRequest<List<SongTitleCountListDto>>;
}