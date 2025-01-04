using MediatR;
using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks;

namespace PopularRadioSongs.Mvc.Controllers
{
    public class ImportsController : Controller
    {
        private readonly ISender _sender;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImportsController(ISender sender, IWebHostEnvironment hostEnvironment)
        {
            _sender = sender;
            _hostEnvironment = hostEnvironment;
        }

        [Route("Imports/{hoursRange:int}")]
        public async Task<IActionResult> Index(ImportPlaybacksCommand importPlaybacksCommand)
        {
            if (!_hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            await _sender.Send(importPlaybacksCommand);

            return View();
        }
    }
}