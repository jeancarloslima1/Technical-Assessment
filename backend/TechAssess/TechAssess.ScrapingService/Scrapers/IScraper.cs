using TechAssess.ScrapingService.Models;

namespace TechAssess.ScrapingService.Scrapers
{
    public interface IScraper
    {
        List<ScrapeData> Scrap(string supplierName);
    }
}
