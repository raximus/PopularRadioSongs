using MediatR;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsList
{
    public class GetArtistsListQueryHandler : IRequestHandler<GetArtistsListQuery, List<ArtistListDto>>
    {
        public async Task<List<ArtistListDto>> Handle(GetArtistsListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}