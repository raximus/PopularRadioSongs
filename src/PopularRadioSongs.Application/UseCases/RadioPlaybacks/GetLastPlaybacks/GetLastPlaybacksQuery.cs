using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks
{
    public record GetLastPlaybacksQuery(int RadioId) : IRequest<UseCaseResult<LastPlaybacksDto>>;
}