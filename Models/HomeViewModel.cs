namespace dotnet_learn_web_mvc.Models
{
    public class HomeViewModel
    {
        public string currentMovie { get; set; }
        public List<string> movies { get; set; }    
        public Dictionary<string, List<string>> tickets { get; set; }

        public int freeSeats = 100;
        public HomeViewModel() 
        {
            movies = new List<string>
            {
                "movie1",
                "movie2",
                "movie3"
            };
        }

        public void AddTicket(string movie)
        {
            if(!tickets.ContainsKey(movie))
            {
                tickets[movie] = new List<string>();
            }
            tickets[movie].Add(String.Format("ticket N: {0} on movie {1}", Random.Shared.NextInt64(), movie));
            if(movie == currentMovie) --freeSeats;
        }

        public void ChangeMovieSession(string movieSession)
        {
            currentMovie = movieSession;
            freeSeats = 100 - tickets[movieSession].Count;
        }

        public void AddMovie(string movie)
        {
            movies.Add(movie);
        }
    }
}
