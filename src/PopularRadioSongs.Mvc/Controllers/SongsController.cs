using MediatR;
using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.UseCases.Songs.GetSongDetails;
using PopularRadioSongs.Application.UseCases.Songs.GetSongsList;
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

        public async Task<IActionResult> Index()
        {
            var songs = await _sender.Send(new GetSongsListQuery());

            return View(songs.Value);
        }

        public async Task<IActionResult> TitleCount()
        {
            var songs = await _sender.Send(new GetSongsTitleCountListQuery());

            return View(songs.Value);
        }

        [Route("Songs/{songId:int}")]
        public async Task<IActionResult> Details(GetSongDetailsQuery songDetailsQuery)
        {
            var song = await _sender.Send(songDetailsQuery);

            return song.IsSuccess ? View(song.Value) : song.FailureToActionResult();
        }
    }
}