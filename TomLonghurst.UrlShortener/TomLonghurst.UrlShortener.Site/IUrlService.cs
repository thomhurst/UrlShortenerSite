using System.Threading.Tasks;

namespace TomLonghurst.UrlShortener.Site
{
    public interface IUrlService
    {
        Task<string> GetUrl(string key);
        Task<string> StoreUrlAndGetKey(string url);
    }
}