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

        public IEnumerable<CastCrew> GetCastCrew()
        {
            return _dbContext.CastCrews.AsEnumerable();
        }

        public IEnumerable<Location> GetLocations()
        {
            return _dbContext.Locations.AsEnumerable();
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _dbContext.Movies.AsEnumerable();
        }

        public IAsyncEnumerable<Movie> GetMoviesAsync()
        {
            return _dbContext.Movies;
        }

        public IEnumerable<Theater> GetTheaters()
        {
            return _dbContext.Theaters.AsEnumerable();
        }
    }
}
