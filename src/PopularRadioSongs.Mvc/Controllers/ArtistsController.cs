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

        [Route("Artists/{artistId:int}")]
        public async Task<IActionResult> Index(GetArtistDetailsQuery artistDetailsQuery)
        {
            var artist = await _sender.Send(artistDetailsQuery);

            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        public async Task<IActionResult> ArtistsByName()
        {
            var artists = await _sender.Send(new GetArtistsListQuery());

            return View(artists);
        }

        public async Task<IActionResult> ArtistsBySongsCount()
        {
            var artists = await _sender.Send(new GetArtistsSongsCountListQuery());

            return View(artists);
        }
    }
}