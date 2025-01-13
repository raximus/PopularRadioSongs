using MediatR;
using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsList;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsSongsCountList;

namespace PopularRadioSongs.Mvc.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly ISender _sender;

        public ArtistsController(ISender sender)
        {
            _sender = sender;
        }

        public async Task<IActionResult> Index(GetArtistsListQuery artistsListQuery)
        {
            var artists = await _sender.Send(artistsListQuery);

            return View(artists.ToPagedViewModel());
        }

        public async Task<IActionResult> SongsCount(GetArtistsSongsCountListQuery artistsSongsCountListQuery)
        {
            var artists = await _sender.Send(artistsSongsCountListQuery);

            return View(artists.ToPagedViewModel());
        }

        [Route("Artists/{artistId:int}")]
        public async Task<IActionResult> Details(GetArtistDetailsQuery artistDetailsQuery)
        {
            var artist = await _sender.Send(artistDetailsQuery);

            return artist.IsSuccess ? View(artist.Value) : artist.FailureToActionResult();
        }
    }
}