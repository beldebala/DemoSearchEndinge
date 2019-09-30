using DemoSearchEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.DataAccessLayer
{
    public interface IRepository
    {
        IEnumerable<Movie> GetMovies();

        IAsyncEnumerable<Movie> GetMoviesAsync();

        void AddMovie(Movie movie);

        IEnumerable<Theater> GetTheaters();

        IEnumerable<Location> GetLocations();

        IEnumerable<CastCrew> GetCastCrew();
    }
}
