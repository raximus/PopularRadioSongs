using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails
{
    public record GetArtistDetailsQuery(int ArtistId) : IRequest<UseCaseResult<ArtistDetailsDto>>;
}