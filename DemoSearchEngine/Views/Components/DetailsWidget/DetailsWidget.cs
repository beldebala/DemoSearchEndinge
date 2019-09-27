using DemoSearchEngine.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.Views.Components.Details
{
    public class DetailsWidget : ViewComponent
    {
        private IRepository _repository;
        public DetailsWidget(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            // add logic to call IElasticSearchService.GetResults

            var result = _repository.GetMovies().Where(x => x.ID == Id).FirstOrDefault();

            return View(result);

        }
    }
}
