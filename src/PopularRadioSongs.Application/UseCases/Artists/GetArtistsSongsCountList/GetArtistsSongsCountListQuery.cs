using MediatR;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList
{
    public record GetArtistsSongsCountListQuery() : IRequest<List<ArtistSongsCountListDto>>;
}