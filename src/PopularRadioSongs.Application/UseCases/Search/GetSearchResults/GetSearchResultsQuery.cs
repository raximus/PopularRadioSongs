using MediatR;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Search.GetSearchResults
{
    public record GetSearchResultsQuery(string SearchValue) : IRequest<UseCaseResult<SearchResultsDto>>;
}