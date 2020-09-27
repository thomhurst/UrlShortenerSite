using Newtonsoft.Json;

namespace TomLonghurst.UrlShortener.Site.Models
{
    public class UrlCosmosModel
    {
        [JsonProperty("id")]
        public string Key { get; set; }
        
        public string Url { get; set; }
    }
}