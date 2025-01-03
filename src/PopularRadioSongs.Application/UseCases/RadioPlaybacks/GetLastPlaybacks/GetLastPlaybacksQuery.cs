using MediatR;

namespace PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks
{
    public record GetLastPlaybacksQuery(int RadioId) : IRequest<LastPlaybacksDto?>;
}