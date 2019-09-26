using DemoSearchEngine.DataAccessLayer;
using DemoSearchEngine.Models;
using DemoSearchEngine.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.Views.Components
{
    public class SearchWidget : ViewComponent
    {
        private ISearchService _searchService;

        private IRepository _repository;
        public SearchWidget(ISearchService searchService, IRepository repository)
        {
            _searchService = searchService;
            _repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string pattern)
        {
            // add logic to call IElasticSearchService.GetResults

            var result =  await _searchService.GetSearchResultsAsync(pattern);

            return View(result);

        }

    }
}
