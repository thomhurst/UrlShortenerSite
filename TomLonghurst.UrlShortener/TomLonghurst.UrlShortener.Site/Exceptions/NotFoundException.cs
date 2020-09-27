using System;

namespace TomLonghurst.UrlShortener.Site.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string key) : base($"Key '{key}' was not found")
        {
        }
    }
}