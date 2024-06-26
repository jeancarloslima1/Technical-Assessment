using TechAssess.ScrapingService.Enums;
using TechAssess.ScrapingService.Scrapers;

namespace TechAssess.ScrapingService.Factories
{
    public interface IScraperFactory
    {
        AbstractScraper GetScraper(Source source);
    }
}
