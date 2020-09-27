using System;
using System.Linq;

namespace TomLonghurst.UrlShortener.Site
{
    public class KeyGenerator : IKeyGenerator
    {
        private static readonly Random Random = new Random();

        public string GenerateUniqueKey()
        {
            return RandomString(6);
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}