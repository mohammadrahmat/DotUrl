using DotUrl.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace DotUrl.Elastic
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration config)
        {
            var url = config["Elasticsearch:Url"];
            var idx = config["Elasticsearch:Index"];

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(idx))
            {
                throw new ArgumentNullException($"Elastic configuration failed, missing url or idx");
            }

            var settings = new ConnectionSettings(new Uri(url)).DefaultIndex(idx);

            AddDefaultMapping(settings);

            var client = new ElasticClient(settings);
            services.AddSingleton(client);

            CreateIndex(client, idx);
        }

        private static void AddDefaultMapping(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<BaseServiceModel>(m => m
                .Ignore(s => s.MerchantId)
                .Ignore(s => s.SearchQuery)
                .Ignore(s => s.HasError)
                .Ignore(s => s.PageType)
                .Ignore(s => s.RecordDate)
                .Ignore(s => s.ContentId)
                .Ignore(s => s.ErrorDetails)
                .Ignore(s => s.ExceptionMessage)
                .Ignore(s => s.Type)
            );
        }

        private static void CreateIndex(IElasticClient client, string idxName)
        {
            client.Indices.Create(idxName,
                idx => idx.Map<BaseServiceModel>(m => m.AutoMap())
            );
        }
    }
}
