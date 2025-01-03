using Microsoft.AspNetCore.Mvc;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Mvc.Models;
using System.Diagnostics;

namespace PopularRadioSongs.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRadioNamesService _radioNamesService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(IRadioNamesService radioNamesService, IWebHostEnvironment hostEnvironment)
        {
            _radioNamesService = radioNamesService;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var homeVM = new HomeViewModel(_radioNamesService.GetRadioStationNames(), _hostEnvironment.IsDevelopment());

            return View(homeVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}