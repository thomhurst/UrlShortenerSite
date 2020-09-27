using System;

namespace TomLonghurst.UrlShortener.Site
{
    public class UrlValidator : IUrlValidator
    {
        public bool IsValid(string url)
        {
            return !string.IsNullOrEmpty(url) && Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}