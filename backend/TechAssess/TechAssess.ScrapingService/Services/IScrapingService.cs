using TechAssess.ScrapingService.Enums;
using TechAssess.ScrapingService.Models;

namespace TechAssess.WebScrapingService.Services
{
    public interface IScrapingService
    {
        IEnumerable<ScrapeResult> Scrap(string supplierName, IEnumerable<Source> sources);
    }
}
