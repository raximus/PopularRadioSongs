using MediatR;
using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.UseCases.Songs.GetSongDetails;
using PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList;

namespace PopularRadioSongs.Mvc.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISender _sender;

        public SongsController(ISender sender)
        {
            _sender = sender;
        }

        [Route("Songs/{songId:int}")]
        public async Task<IActionResult> Index(GetSongDetailsQuery songDetailsQuery)
        {
            var song = await _sender.Send(songDetailsQuery);

            if (song is null)
            {
                return NotFound();
            }

            return View(song);
        }

        public async Task<IActionResult> SongsByTitleCount()
        {
            var songs = await _sender.Send(new GetSongsTitleCountListQuery());

            return View(songs);
        }
    }
}