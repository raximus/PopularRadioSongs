using MediatR;
using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.UseCases.Search.GetSearchResults;

namespace PopularRadioSongs.Mvc.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISender _sender;

        public SearchController(ISender sender)
        {
            _sender = sender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Results(GetSearchResultsQuery searchResultsQuery)
        {
            var searchResults = await _sender.Send(searchResultsQuery);

            return View(searchResults);
        }
    }
}