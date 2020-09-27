using System.Threading.Tasks;

namespace TomLonghurst.UrlShortener.Site
{
    public class UrlService : IUrlService
    {
        private readonly IDatabase _database;
        private readonly IKeyGenerator _keyGenerator;

        public UrlService(IDatabase database, IKeyGenerator keyGenerator)
        {
            _database = database;
            _keyGenerator = keyGenerator;
        }
        
        public Task<string> GetUrl(string key)
        {
            return _database.GetUrl(key);
        }

        public async Task<string> StoreUrlAndGetKey(string url)
        {
            var uniqueKey = _keyGenerator.GenerateUniqueKey();
            
            while (await _database.KeyExists(uniqueKey))
            {
                uniqueKey = _keyGenerator.GenerateUniqueKey();
            }

            await _database.StoreUrl(uniqueKey, url);

            return uniqueKey;
        }
    }
}