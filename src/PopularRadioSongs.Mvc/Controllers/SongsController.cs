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

        public async Task<IActionResult> Index(GetSongsListQuery songsListQuery)
        {
            var songs = await _sender.Send(songsListQuery);

            return View(songs.ToPagedViewModel());
        }

        public async Task<IActionResult> TitleCount(GetSongsTitleCountListQuery songsTitleCountListQuery)
        {
            var songs = await _sender.Send(songsTitleCountListQuery);

            return View(songs.ToPagedViewModel());
        }

        [Route("Songs/{songId:int}")]
        public async Task<IActionResult> Details(GetSongDetailsQuery songDetailsQuery)
        {
            var song = await _sender.Send(songDetailsQuery);

            return song.IsSuccess ? View(song.Value) : song.FailureToActionResult();
        }
    }
}