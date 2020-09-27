using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using TomLonghurst.UrlShortener.Site.Models;

namespace TomLonghurst.UrlShortener.Site
{
    public class CosmosDatabase : IDatabase
    {
        private readonly CosmosClient _client;
        private Container _container;

        public CosmosDatabase()
        {
            _client = new CosmosClient("https://localhost:8081",
                    "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
        }


        public async Task<string> GetUrl(string key)
        {
            try
            {
                var container = await GetContainer();
                var cosmosResponse = await container.ReadItemAsync<UrlCosmosModel>(key, new PartitionKey(key));
            
                return cosmosResponse.Resource.Url;
            }
            catch (CosmosException cosmosException) when(cosmosException.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task StoreUrl(string key, string url)
        {
            var urlCosmosModel = new UrlCosmosModel
            {
                Key = key,
                Url = url
            };
            
            var container = await GetContainer();

            await container.CreateItemAsync(urlCosmosModel, new PartitionKey(key));
        }

        public async Task<bool> KeyExists(string key)
        {
            var url = await GetUrl(key);
            return !string.IsNullOrWhiteSpace(url);
        }

        private async Task<Container> GetContainer()
        {
            if (_container != null)
            {
                return _container;
            }
            
            var database = await _client.CreateDatabaseIfNotExistsAsync("url-shortener");

            return _container = await database.Database.CreateContainerIfNotExistsAsync("urls", "/id");
        }
    }
}