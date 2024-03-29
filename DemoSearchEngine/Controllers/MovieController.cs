﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSearchEngine.DataAccessLayer;
using DemoSearchEngine.Models;
using DemoSearchEngine.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoSearchEngine.Controllers
{
    public class MovieController : Controller
    {
        private IRepository _repository;

        private ISearchService _searchService;

        public MovieController(IRepository repository, ISearchService searchService)
        {
            _repository = repository;
            _searchService = searchService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/Movie/Add")]
        public void AddMovie(Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.AddMovie(movie);
                    _searchService.IndexMovieAsync(movie);
                }
                else
                {
                    RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                //log exception
            }
            
        }

        [HttpGet("/Movie/Get")]
        public IActionResult MovieDetails(int Id)
        {
            return ViewComponent("DetailsWidget", Id);
        }
    }
}