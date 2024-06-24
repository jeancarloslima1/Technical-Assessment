using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechAssess.ScrapingService.Enums;
using TechAssess.WebScrapingService.Services;

namespace TechAssess.WebScrapingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapingController : ControllerBase
    {
        private readonly IScrapingService _scrapingService;

        public ScrapingController(IScrapingService scrapingService)
        {
            _scrapingService = scrapingService;
        }


        [HttpGet]
        public IActionResult GetScrapingResults([FromQuery] string supplierName, [FromQuery] List<Source> sources)
        {
            var results = _scrapingService.Scrap(supplierName, sources);
            return Ok(results);
        }
    }
}
