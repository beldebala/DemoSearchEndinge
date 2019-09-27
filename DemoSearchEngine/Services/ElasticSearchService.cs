using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSearchEngine.Models;
using Microsoft.Extensions.Logging;
using Nest;

namespace DemoSearchEngine.Services
{
    public class ElasticSearchService : ISearchService
    {
        private IElasticClient _elasticClient;
        private ILogger<ElasticSearchService> _logger;
        private const int BATCH_SIZE = 10;
        public ElasticSearchService(IElasticClient elasticClient, ILogger<ElasticSearchService> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task IndexMovieAsync(IEnumerable<Movie> movies)
        {
            try
            {
                var bulkIndexResponse = await _elasticClient.BulkAsync(b => b
                                              .Index("movieindex")
                                              .IndexMany(movies));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }            
        }

        public async Task IndexMovieAsync(Movie movie)
        {
            try
            {
                var bulkIndexResponse = await _elasticClient.IndexAsync(movie, i => i.Index("movieindex"));
                                                       
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        public async Task IndexTheaterAsync(Theater theater)
        {
            try
            {
                var bulkIndexResponse = await _elasticClient.IndexAsync(theater, i => i.Index("theaterindex"));

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        public async Task<List<SearchResult>> GetSearchResultsAsync(string pattern)
        {
            List<SearchResult> searchResults = null;
            try
            {
                var result = await _elasticClient.SearchAsync<SearchResult>(s => s
                                           .Query(q => q.MultiMatch(c => c
                                                        .Fields(
                                                                fs => fs.Field(f => f.Name)
                                                                        .Field(f => f.Genre)
                                                                )
                                                        .Type(TextQueryType.PhrasePrefix)
                                                        .Analyzer("standard")
                                                        .Boost(1.1)
                                                        .Query(pattern)
                                                        .MaxExpansions(2)
                                                        .Slop(2)
                                                        .Name("named_query")
                                                    ))
                                           .Index("_all")
                                        );

                searchResults = result.Documents.ToList<SearchResult>();
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return searchResults;
        }

        public async Task IndexTheaterAsync(IEnumerable<Theater> theaters)
        {
            try
            {
                var bulkIndexResponse = await _elasticClient.BulkAsync(b => b
                                              .Index("theaterindex")
                                              .IndexMany(theaters));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }
    }
}
