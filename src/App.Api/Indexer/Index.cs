using App.Api.Documents;
using Microsoft.Extensions.Configuration;
using Nest;
using System;

namespace App.Api.Indexer
{
    public class Index : IIndex
    {
        private IConfiguration _configuration;

        public Index(IConfiguration configuration)
        {
            _configuration = configuration;
        }     

        public void Add(PasswordDetails password)
        {
            var elasticsearchUrl = _configuration["Elasticsearch:Url"];
            EnsureIndex(elasticsearchUrl);
            try
            {
                var node = new Uri(elasticsearchUrl);
                var client = new ElasticClient(node);                
                client.Index(password, idx => idx.Index("passwords"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Index password FAILED, ex: {ex}");
            }
        }
        
        private static void EnsureIndex(string elasticsearchUrl)
        {
            try
            {
                var node = new Uri(elasticsearchUrl);
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);
            client.CreateIndex("passwords");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create index FAILED, ex: {ex}");
            }
        }
    }
}
