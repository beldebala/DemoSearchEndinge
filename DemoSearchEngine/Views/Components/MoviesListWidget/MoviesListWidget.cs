using DemoSearchEngine.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.Views.Components.Movies
{
    public class MoviesListWidget : ViewComponent
    {
        private IRepository _repository;
        public MoviesListWidget(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // add logic to call IElasticSearchService.GetResults

            var result = _repository.GetMoviesAsync();

            return View(result);

        }
    }
}
