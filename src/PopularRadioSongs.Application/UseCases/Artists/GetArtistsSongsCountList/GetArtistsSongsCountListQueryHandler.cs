using MediatR;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList
{
    public class GetArtistsSongsCountListQueryHandler : IRequestHandler<GetArtistsSongsCountListQuery, List<ArtistSongsCountListDto>>
    {
        public async Task<List<ArtistSongsCountListDto>> Handle(GetArtistsSongsCountListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}