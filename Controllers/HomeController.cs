using dotnet_learn_web_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CinemaSpace.Models;


namespace CinemaSpace.Controllers
{
    public class HomeController : Controller
    {
        // Model for the cinema
        public static Cinema CinemaModel {
            get {
                if (_cinemaModel is null)
                    _cinemaModel = new Cinema();
                return _cinemaModel;
            }
        }
        private static Cinema? _cinemaModel;
        public static CinemaSpace.Models.Hall _currentHallView { get; set; } = CinemaModel.GetHalls().FirstOrDefault();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           
        }

        public IActionResult Index()
        {
            ViewData.Model = CinemaModel;
            ViewData["Movies"] = CinemaModel.GetMovieNames();
            ViewData["Halls"] = CinemaModel.GetHalls();
            ViewData["CurrentHallView"] = _currentHallView;
            ViewData["TakenSeats"] = CinemaModel.GetTakenSeats(_currentHallView.HallNumber);
            return View();
        }

        public IActionResult HallView(CinemaSpace.Models.Hall inHall)
        {
            ViewData["Halls"] = CinemaModel.GetHalls();
            ViewData["CurrentHallView"] = _currentHallView;
            ViewData["TakenSeats"] = CinemaModel.GetTakenSeats(inHall.HallNumber);
            CinemaSpace.Models.Hall? choosenHall = CinemaModel.GetHalls().Find(hall => hall.HallNumber == inHall.HallNumber);
            if (choosenHall is null)
                return RedirectToAction("Index", "Home");

            _currentHallView = choosenHall;
            return RedirectToAction("Index", "Home");
        }   
        public IActionResult CreateTicket(CinemaSpace.Models.MovieTicket ticket)
        {
            //Get all movies from database
            ViewData["Movies"] = CinemaModel.GetMovies().ToList();

            if(!IsValidName(ticket.MovieName))
                return View();
            
            ViewData["SelectedHall"] = CinemaModel.GetHalls().Find(hall => hall.HallNumber == ticket.HallNumber);
            ViewData["Halls"] = CinemaModel.GetHalls().FindAll(hall => hall.MovieName == ticket.MovieName);
            
            bool IsSeatAlreadyTaken = false;
            bool TicketAdded = CinemaModel.AddTicket(ticket.MovieName, ticket.HallNumber, ticket.SeatNumber, out IsSeatAlreadyTaken);
            
            if(!TicketAdded)
            {
                ViewData["IsSeated"] = IsSeatAlreadyTaken;
                return View();
            }
            //Redirect to the home page
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangeMovieSession(CinemaSpace.Models.Movie Movie)
        {
            // Get all movies from the database and display them in a list
            ViewData["Movies"] =  CinemaModel.GetMovies().ToList();
            // Check if the movie is valid
            if (!Cinema.IsValidMovie(Movie))
                // If the movie is not valid, return the current view
                return View();
            // Update the hall movie session
            CinemaModel.UpdateHallMovieSession(0, Movie.MovieName);
            // Redirect to the home page
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
            CinemaModel.AddMovie(Movie.MovieName);
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


        bool IsValidName(string? name)
        {
            return name != null && !string.IsNullOrWhiteSpace(name) && name != "Empty";
        }
    }
}