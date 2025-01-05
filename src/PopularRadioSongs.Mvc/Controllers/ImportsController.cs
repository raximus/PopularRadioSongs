using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PopularRadioSongs.Application.Options;
using PopularRadioSongs.Application.UseCases.Imports.ImportPlaybacks;

namespace PopularRadioSongs.Mvc.Controllers
{
    public class ImportsController : Controller
    {
        private readonly ISender _sender;
        private readonly AppOptions _appOptions;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImportsController(ISender sender, IOptions<AppOptions> appOptions, IWebHostEnvironment hostEnvironment)
        {
            _sender = sender;
            _appOptions = appOptions.Value;
            _hostEnvironment = hostEnvironment;
        }

        [Route("Imports/{hoursRange:int}")]
        public async Task<IActionResult> Index(ImportPlaybacksCommand importPlaybacksCommand)
        {
            if (!_appOptions.ManualImportOnProduction && !_hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            await _sender.Send(importPlaybacksCommand);

            return View();
        }
    }
}