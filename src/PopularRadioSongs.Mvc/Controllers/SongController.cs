using MediatR;
using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.UseCases.Songs.GetSongDetails;
using PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList;

namespace PopularRadioSongs.Mvc.Controllers
{
    public class SongController : Controller
    {
        private readonly ISender _sender;

        public SongController(ISender sender)
        {
            _sender = sender;
        }

        [Route("Song/{songId:int}")]
        public async Task<IActionResult> Index(int songId)
        {
            var song = await _sender.Send(new GetSongDetailsQuery(songId));

            if (song == null)
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