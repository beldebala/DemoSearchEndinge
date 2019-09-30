using DemoSearchEngine.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.Extenstions
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:defaultindex"];

           var settings = new ConnectionSettings(new Uri(url))
                                    .DefaultIndex(defaultIndex);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateMappings(client);
        }

        private static void CreateMappings(ElasticClient client)
        {
            client.Indices.Create("movieindex", c => c
               .Map<Movie>(m => m.AutoMap()));

            client.Indices.Create("theaterindex", c => c
               .Map<Theater>(m => m.AutoMap()));

            client.Indices.Create("locationindex", c => c
               .Map<Location>(m => m.AutoMap()));

            client.Indices.Create("castcrewindex", c => c
              .Map<CastCrew>(m => m.AutoMap()));
        }
    }
}
