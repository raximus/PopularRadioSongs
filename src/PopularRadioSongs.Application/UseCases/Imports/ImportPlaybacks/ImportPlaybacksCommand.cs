using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks
{
    public record ImportPlaybacksCommand(int HoursRange) : IRequest<UseCaseResult>;
}