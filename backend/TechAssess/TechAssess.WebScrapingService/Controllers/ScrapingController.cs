using Microsoft.AspNetCore.Mvc;
using TechAssess.ScrapingService.Attributes;
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
        [Validation]
        public IActionResult GetScrapingResults([FromQuery] string supplierName, [FromQuery] List<Source> sources)
        {
            try
            {
                var results = _scrapingService.Scrap(supplierName, sources);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
