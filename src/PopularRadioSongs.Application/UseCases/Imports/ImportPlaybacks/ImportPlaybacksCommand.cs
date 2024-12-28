using MediatR;

namespace PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks
{
    public record ImportPlaybacksCommand(int HoursRange) : IRequest;
}