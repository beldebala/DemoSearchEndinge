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
        Task<Dictionary<string, List<SearchResult>>> GetSearchResultsAsync5(string pattern);

        Task<Dictionary<string, List<SearchResult>>> GetSearchResultsAsync(string pattern);
        Task IndexMovieAsync(Movie movie);

        Task IndexTheaterAsync(Theater theater);

        Task IndexTheaterAsync(IEnumerable<Theater> theaters);

        Task IndexLocationsAsync(IEnumerable<Location> locations);

        Task IndexCastCrewAsync(IEnumerable<CastCrew> castCrews);
    }
}
