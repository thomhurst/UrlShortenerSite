using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TomLonghurst.UrlShortener.Site.Models;

namespace TomLonghurst.UrlShortener.Site.Controllers
{
    public class UrlController : Controller
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IUrlService _urlService;
        private readonly IUrlValidator _validator;

        public UrlController(ILogger<UrlController> logger, IUrlService urlService, IUrlValidator validator)
        {
            _logger = logger;
            _urlService = urlService;
            _validator = validator;
        }

        [HttpGet("/{key}")]
        public async Task<IActionResult> FromPath([FromRoute] string key)
        {
            var url = await _urlService.GetUrl(key);

            if (string.IsNullOrWhiteSpace(url))
            {
                // TODO - Not Found View Page
                return NotFound();
            }
            
            return Redirect(url);
        }

        [HttpPost("/generate")]
        public async Task<IActionResult> Generate([FromBody] GenerateUrlModel urlModel)
        {
            if (!_validator.IsValid(urlModel.Url))
            {
                return BadRequest();
            }
            
            var key = await _urlService.StoreUrlAndGetKey(urlModel.Url);
            
            return Ok(key);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}