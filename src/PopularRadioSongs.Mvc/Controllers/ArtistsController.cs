﻿using MediatR;
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

        public async Task<IActionResult> Index()
        {
            var artists = await _sender.Send(new GetArtistsListQuery());

            return View(artists.Value);
        }

        public async Task<IActionResult> SongsCount()
        {
            var artists = await _sender.Send(new GetArtistsSongsCountListQuery());

            return View(artists.Value);
        }

        [Route("Artists/{artistId:int}")]
        public async Task<IActionResult> Details(GetArtistDetailsQuery artistDetailsQuery)
        {
            var artist = await _sender.Send(artistDetailsQuery);

            return artist.IsSuccess ? View(artist.Value) : artist.FailureToActionResult();
        }
    }
}