using System.Reflection;

namespace dotnet_learn_web_mvc.Models
{
    public class HomeViewModel
    {
        public string currentMovie;
        public List<MovieModel> movies { get; set; }    
        public Dictionary<string, List<string>> tickets { get; set; }

        public int Seats = 100;
        public int freeSeats = 100;
        static HomeViewModel? model_;
        static public HomeViewModel GetModel()
        {
            if (model_ == null)
                model_ = new HomeViewModel();

            return model_;
        }
        public HomeViewModel() 
        {
            movies = new List<MovieModel>
            {
                new MovieModel{ Name = "Movie1"},
                new MovieModel{ Name = "Movie2"},
                new MovieModel{ Name = "Movie3"},
                new MovieModel{ Name = "Empty"}
            };
            tickets = new Dictionary<string, List<string>>();
        }

        public void AddTicket(string movieName)
        {
            if(!tickets.ContainsKey(movieName))
            {
                tickets[movieName] = new List<string>();
            }
            tickets[movieName].Add(String.Format("ticket N: {0} on movie {1}", Random.Shared.NextInt64(), movieName));
            if(movieName == currentMovie) --freeSeats;
        }

        public void ChangeMovieSession(string movieName)
        {
            currentMovie = movieName;
            if (!tickets.ContainsKey(movieName))
            {
                freeSeats = 100;
                return;
            }

            freeSeats = 100 - tickets[movieName].Count;
        }

        public void AddMovie(MovieModel movie)
        {
            movies.Add(movie);
        }
    }
}
