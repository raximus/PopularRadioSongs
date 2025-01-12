using MediatR;
using PopularRadioSongs.Application.UseCases.Search.GetSearchResults;

namespace PopularRadioSongs.Api.Endpoints
{
    public static class SearchEndpoints
    {
        public static void RegisterSearchEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapGet("/search", GetSearchResults)
                .WithName("GetSearchResults").WithSummary("Get Search Results")
                .Produces<SearchResultsDto>().ProducesValidationProblem();
        }

        static async Task<IResult> GetSearchResults([AsParameters] GetSearchResultsQuery searchResultsQuery, ISender sender)
        {
            var searchResults = await sender.Send(searchResultsQuery);

            return searchResults.IsSuccess ? TypedResults.Ok(searchResults.Value) : searchResults.FailureToMinimalApi();
        }
    }
}