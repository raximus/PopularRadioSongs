using MediatR;
using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks;

namespace PopularRadioSongs.Mvc.Controllers
{
    public class RadioPlaybacksController : Controller
    {
        private readonly ISender _sender;

        public RadioPlaybacksController(ISender sender)
        {
            _sender = sender;
        }

        [Route("RadioPlaybacks/{radioId:int}")]
        public async Task<IActionResult> Index(GetLastPlaybacksQuery lastPlaybacksQuery)
        {
            var lastPlaybacks = await _sender.Send(lastPlaybacksQuery);

            if (lastPlaybacks == null)
            {
                return NotFound();
            }

            return View(lastPlaybacks);
        }
    }
}