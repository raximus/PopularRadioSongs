using MediatR;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails
{
    public record GetArtistDetailsQuery(int ArtistId) : IRequest<ArtistDetailsDto?>;
}