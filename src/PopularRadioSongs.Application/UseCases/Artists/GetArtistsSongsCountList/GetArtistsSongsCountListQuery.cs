using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList
{
    public record GetArtistsSongsCountListQuery() : IRequest<UseCaseResult<List<ArtistSongsCountListDto>>>;
}