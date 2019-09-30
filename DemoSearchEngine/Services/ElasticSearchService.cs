using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSearchEngine.Enums;
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

        public async Task<Dictionary<string, List<SearchResult>>> GetSearchResultsAsync(string pattern)
        {
            Dictionary<string, List<SearchResult>> keyValuePairs = new Dictionary<string, List<SearchResult>>();
            List<SearchResult> searchResults = null;
            try
            {
                //var searchRequest = new SearchRequest<SearchResult>("_all")
                //{
                //    Aggregations = new FilterAggregation("my_filter")
                //    {
                //         Filter = new TermQuery()
                //         {
                //             Field = "name",
                //             Value = pattern
                //         },
                //        Aggregations = new TermsAggregation("by_categories")
                //        {
                //            Field = new Field("Category") { },

                //            Aggregations = new TopHitsAggregation("my_top_hits")
                //            {
                //                Source = new SourceFilter
                //                {
                //                    Includes = new[] { "name", "id", "category" }
                //                },
                //                Size = 10
                //            }
                //        }
                //    },
                //    AllowNoIndices = true
                //};

                // var searchResponse = await _elasticClient
                // .SearchAsync<SearchResult>(searchRequest);

                var result = await _elasticClient.SearchAsync<SearchResult>(s => s
                                        .Aggregations(a => a.Filter("search_filter", f => f.Filter(
                                            q => q.MultiMatch(c => c
                                                        .Fields(
                                                                fs => fs.Field(f => f.Name)
                                                                        .Field(f => f.Genre)
                                                                        .Field(f => f.FirstName)
                                                                        .Field(f => f.LastName)
                                                                )
                                                        .Type(TextQueryType.PhrasePrefix)
                                                        .Analyzer("standard")
                                                        .Boost(1.1)
                                                        .Query(pattern)
                                                        .MaxExpansions(2)
                                                        .Slop(2)
                                                        .Name("named_query")))                                        
                                                    .Aggregations(
                                                            childaggs => childaggs.Terms("by_categories",
                                                                                          x => x.Field(p => p.Category).Size(10)
                                                                                                .Aggregations(th => th.TopHits("top_hits",h => h.Source(s => s.IncludeAll())))
                                                                                         ))))
                                           .Index("_all")
                                        );

                var filterresults = result.Aggregations.Filter("search_filter");
                var buckets = filterresults?.Terms("by_categories")?.Buckets;
               
                foreach (var b in buckets)
                {
                    var tophits = b.TopHits("top_hits");
                    var values = tophits.Documents<SearchResult>().ToList();
                    string key = values.FirstOrDefault().Category.ToString();
                    keyValuePairs.Add(key, values);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

          

            return keyValuePairs;
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


        public async Task<Dictionary<string, List<SearchResult>>> GetSearchResultsAsync5(string pattern)
        {
            Dictionary<string, List<SearchResult>> keyValuePairs = new Dictionary<string, List<SearchResult>>();

            List<dynamic> searchResults = new List<dynamic>();
            IReadOnlyCollection<KeyedBucket<string>> buckets = null;
            try
            {
                var response = await _elasticClient.SearchAsync<SearchResult>(s => s
                //Math prefix phrase query to filter the documents
                                           .Query(q => q.MultiMatch(c => c
                                                        .Fields(
                                                                fs => fs//.Field(f => f.Name)
                                                                        //.Field(f => f.Genre)
                                                                        .Field(f => f.FirstName)
                                                                        .Field(f => f.LastName)
                                                                )
                                                        .Lenient(true)
                                                        .Type(TextQueryType.PhrasePrefix)
                                                        .Type(TextQueryType.MostFields)
                                                        .Analyzer("standard")
                                                        .Boost(1.1)
                                                        .Query(pattern)
                                                        .MaxExpansions(2)
                                                        .Slop(2)
                                                        .Name("named_query")
                                                    )
                                           )

                                           //Term Query to bucket documents by category
                                           .Aggregations(
                                                          a => a
                                                          .Terms("top_tags",
                                                               t => t.Field("category")
                                                               .Aggregations(
                                                                   aa => aa.TopHits("top_category_hits",
                                                                                   th => th.Source(
                                                                                       src => src.IncludeAll())))
                                                               )
                                                          )                                           
                                           .Index("_all")
                                        );
                
                buckets = response.Aggregations.Terms("top_tags").Buckets;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            foreach (var b in buckets)
            {
                var tophits = b.TopHits("top_category_hits");
                var values = tophits.Documents<SearchResult>().ToList();
                string key = values.FirstOrDefault().Category.ToString();

                keyValuePairs.Add(key, values);

            }

           return keyValuePairs;            
        }

        public async Task IndexLocationsAsync(IEnumerable<Location> locations)
        {
            try
            {
                var bulkIndexResponse = await _elasticClient.BulkAsync(b => b
                                              .Index("locationindex")
                                              .IndexMany(locations));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        public async Task IndexCastCrewAsync(IEnumerable<CastCrew> castCrews)
        {
            try
            {
                var bulkIndexResponse = await _elasticClient.BulkAsync(b => b
                                              .Index("castcrewindex")
                                              .IndexMany(castCrews));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }
    }
}
