using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongDetails
{
    public record GetSongDetailsQuery(int SongId) : IRequest<UseCaseResult<SongDetailsDto>>;
}