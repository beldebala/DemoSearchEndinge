﻿using DemoSearchEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.DataAccessLayer
{
    public interface IRepository
    {
        List<Movie> GetMovies();

        IAsyncEnumerable<Movie> GetMoviesAsync();

        void AddMovie(Movie movie);

        List<Theater> GetTheaters();

        List<Location> GetLocations();

        List<CastCrew> GetCastCrew();
    }
}
