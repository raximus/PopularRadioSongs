using MediatR;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails
{
    public class GetArtistDetailsQueryHandler : IRequestHandler<GetArtistDetailsQuery, ArtistDetailsDto?>
    {
        public async Task<ArtistDetailsDto?> Handle(GetArtistDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}