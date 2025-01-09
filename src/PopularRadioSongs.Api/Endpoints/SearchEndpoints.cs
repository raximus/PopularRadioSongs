using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PopularRadioSongs.Application.UseCases.Search.GetSearchResults;

namespace PopularRadioSongs.Api.Endpoints
{
    public static class SearchEndpoints
    {
        public static void RegisterSearchEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapGet("/search", GetSearchResults).WithName("GetSearchResults").WithSummary("Get Search Results");
        }

        static async Task<Results<Ok<SearchResultsDto>, ValidationProblem>> GetSearchResults([AsParameters] GetSearchResultsQuery searchResultsQuery, ISender sender)
        {
            if (searchResultsQuery.SearchValue.Length < 3)
            {
                var validationProblem = new Dictionary<string, string[]>
                    {
                        { "SearchValue", new string[] { "Search Value must have at least 3 characters" } }
                    };
                return TypedResults.ValidationProblem(validationProblem);
            }

            var searchResults = await sender.Send(searchResultsQuery);

            return TypedResults.Ok(searchResults);
        }
    }
}