using MediatR;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongDetails
{
    public record GetSongDetailsQuery(int SongId) : IRequest<SongDetailsDto?>;
}