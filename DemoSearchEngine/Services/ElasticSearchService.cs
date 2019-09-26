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
                var searchResponse = await _elasticClient.SearchAsync<Movie, Theater>(s => s
                                           .Query(q => q.MatchPhrasePrefix(c => c
                                                           .Field(p => p.Name)
                                                           .Analyzer("standard")
                                                           .Boost(1.1)
                                                           .Name("named_query")
                                                           .Query(pattern)
                                                           
                                                       ) )
                                          .Aggregations(
                                                a => a.Filters("by_category",
                                                agg => agg.OtherBucket()
                                                          .OtherBucketKey("other_search_by_category")
                                                          .NamedFilters(filters => filters
                                                          .Filter("Movies", f => f.Term(p => p.Category, "Movies"))
                                                          .Filter("Theaters", f => f.Term(p => p.Category, "Theater")))
                                                          ))
                                           .Index("_all")
                                        );

                searchResults = searchResponse
                    .Documents?.Select(x => new SearchResult()
                    {
                        Category = "Movies",
                        Name = x.Name
                    }).ToList();

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
