using MediatR;

namespace PopularRadioSongs.Application.UseCases.Search.GetSearchResults
{
    public record GetSearchResultsQuery(string SearchValue) : IRequest<SearchResultsDto>;
}