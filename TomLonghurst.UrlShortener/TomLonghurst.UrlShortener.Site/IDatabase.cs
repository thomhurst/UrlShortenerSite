using System.Threading.Tasks;

namespace TomLonghurst.UrlShortener.Site
{
    public interface IDatabase
    {
        Task<string> GetUrl(string key);
        Task StoreUrl(string key, string url);
        Task<bool> KeyExists(string key);
    }
}