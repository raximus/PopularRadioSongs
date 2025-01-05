using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Options;
using PopularRadioSongs.Mvc.Models;
using System.Diagnostics;

namespace PopularRadioSongs.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRadioNamesService _radioNamesService;
        private readonly AppOptions _appOptions;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(IRadioNamesService radioNamesService, IOptions<AppOptions> appOptions, IWebHostEnvironment hostEnvironment)
        {
            _radioNamesService = radioNamesService;
            _appOptions = appOptions.Value;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var homeVM = new HomeViewModel(_radioNamesService.GetRadioStationNames(), _appOptions.ManualImportOnProduction || _hostEnvironment.IsDevelopment());

            return View(homeVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}