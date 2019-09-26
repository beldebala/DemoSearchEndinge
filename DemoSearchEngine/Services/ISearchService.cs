using DemoSearchEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.Services
{
    public interface ISearchService
    {
        Task IndexMovieAsync(IEnumerable<Movie> movies);
        Task<List<SearchResult>> GetSearchResultsAsync(string pattern);
        Task IndexMovieAsync(Movie movie);

        Task IndexTheaterAsync(Theater theater);

        Task IndexTheaterAsync(IEnumerable<Theater> theaters);
    }
}
