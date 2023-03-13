using dotnet_learn_web_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace dotnet_learn_web_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HomeViewModel _viewModel;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _viewModel = HomeViewModel.GetModel();
        }

        public IActionResult Index()
        {
            ViewData.Model = _viewModel;
            ViewData["Movies"] = _viewModel.movies;
            return View();
        }
        public IActionResult CreateTicket(string Movie)
        {
            ViewData["Movies"] = _viewModel.movies;
            if (Movie == null)
                return View();
            _viewModel.AddTicket(Movie);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangeMovieSession(string Movie)
        {
            ViewData["Movies"] = _viewModel.movies;
            if (Movie == null)
                return View();
            _viewModel.ChangeMovieSession(Movie);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddMovie(MovieModel Movie)
        {
            if (Movie == null || Movie.Name == null)
                return View();

            _viewModel.AddMovie(Movie);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}