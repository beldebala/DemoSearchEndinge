using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSearchEngine.Models;
using Microsoft.Extensions.Logging;

namespace DemoSearchEngine.DataAccessLayer
{
    public class Repository : IRepository
    {
        private MoviesDBContext _dbContext;

        private ILogger<Repository> _logger;

        public Repository(MoviesDBContext dBContext, ILogger<Repository> logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }
        public void AddMovie(Movie movie)
        {
            try
            {
                _dbContext.Movies.Add(movie);
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        public List<CastCrew> GetCastCrew()
        {
            return _dbContext.CastCrews?.ToList<CastCrew>();
        }

        public List<Location> GetLocations()
        {
            return _dbContext.Locations?.ToList<Location>();
        }

        public List<Movie> GetMovies()
        {
            return _dbContext.Movies?.ToList<Movie>();
        }

        public IAsyncEnumerable<Movie> GetMoviesAsync()
        {
            return _dbContext.Movies;
        }

        public List<Theater> GetTheaters()
        {
            return _dbContext.Theaters?.ToList<Theater>();
        }
    }
}
