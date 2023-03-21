using dotnet_learn_web_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CinemaSpace.Models;


namespace CinemaSpace.Controllers
{
    public class HomeController : Controller
    {
        private Cinema _cinemaModel = new Cinema();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData.Model = _cinemaModel;
            ViewData["Movies"] = _cinemaModel.GetMovieNames();
            ViewData["Halls"] = _cinemaModel.GetHalls();
            return View();
        }
        public IActionResult CreateTicket(CinemaSpace.Models.Movie Movie)
        {
            ViewData["Movies"] = _cinemaModel.GetMovies().ToList();
            if (Movie == null || Movie.MovieName == null || Movie.MovieName.Trim() == "")
                return View();
            _cinemaModel.AddTicket(Movie.MovieName);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangeMovieSession(CinemaSpace.Models.Movie Movie)
        {
            ViewData["Movies"] = _cinemaModel.GetMovieNames();
            if (!Cinema.IsValidMovie(Movie))
                return View();
            _cinemaModel.UpdateHallMovieSession(0, Movie.MovieName);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddMovie(CinemaSpace.Models.Movie Movie)
        {
            if (!Cinema.IsValidMovie(Movie))
                return View();

            _cinemaModel.AddMovie(Movie.MovieName);
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