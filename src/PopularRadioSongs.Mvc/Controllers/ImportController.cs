using MediatR;
using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks;

namespace PopularRadioSongs.Mvc.Controllers
{
    public class ImportController : Controller
    {
        private readonly ISender _sender;

        public ImportController(ISender sender)
        {
            _sender = sender;
        }

        [Route("Import/{hoursRange:int}")]
        public async Task<IActionResult> Index(int hoursRange)
        {
            await _sender.Send(new ImportPlaybacksCommand(hoursRange));

            return View();
        }
    }
}