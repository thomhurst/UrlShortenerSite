namespace TomLonghurst.UrlShortener.Site
{
    public interface IUrlValidator
    {
        bool IsValid(string url);
    }
}