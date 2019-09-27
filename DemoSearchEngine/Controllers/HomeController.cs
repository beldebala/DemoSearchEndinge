using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoSearchEngine.Models;
using DemoSearchEngine.DataAccessLayer;
using DemoSearchEngine.Services;

namespace DemoSearchEngine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IRepository _repository;

        private ISearchService _searchService;
        public HomeController(ILogger<HomeController> logger,
            IRepository repository, ISearchService searchService)
        {
            _logger = logger;
            _repository = repository;
            _searchService = searchService;
        }

        public async Task<IActionResult> Index()
        {
            // var result = await _searchService.GetSearchResultsAsync("");
            var result = _repository.GetMovies();
            await _searchService.IndexMovieAsync(result);

            var theaters = _repository.GetTheaters();
            await _searchService.IndexTheaterAsync(theaters);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Search(string pattern)
        {
            return ViewComponent("SearchWidget", pattern);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Details(int Id)
        {
            return ViewComponent("DetailsWidget", Id);
        }

    }
}
