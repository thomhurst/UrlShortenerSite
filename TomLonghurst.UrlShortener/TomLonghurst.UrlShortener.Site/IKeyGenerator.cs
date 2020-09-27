namespace TomLonghurst.UrlShortener.Site
{
    public interface IKeyGenerator
    {
        string GenerateUniqueKey();
    }
}