using MediatR;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsList
{
    public record GetArtistsListQuery() : IRequest<List<GroupArtistListDto>>;
}