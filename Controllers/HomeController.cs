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
            //Get all movies from database
            ViewData["Movies"] = _cinemaModel.GetMovies().ToList();

            //Check if the movie is valid
            if (!Cinema.IsValidMovie(Movie))
                return View();

            //Add a ticket to the movie
            _cinemaModel.AddTicket(Movie.MovieName);

            //Redirect to the home page
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

        // This function will add a new movie to the cinema model
        public IActionResult AddMovie(CinemaSpace.Models.Movie Movie)
        {
            // First, we check if the movie is valid
            if (!Cinema.IsValidMovie(Movie))
                // If the movie is not valid, we return the view
                return View();

            // If the movie is valid, we add it to the model
            _cinemaModel.AddMovie(Movie.MovieName);
            // We return to the home page
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