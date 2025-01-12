using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsList
{
    public record GetArtistsListQuery() : IRequest<UseCaseResult<List<GroupArtistListDto>>>;
}