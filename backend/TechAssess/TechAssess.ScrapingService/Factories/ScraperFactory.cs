using TechAssess.ScrapingService.Enums;
using TechAssess.ScrapingService.Scrapers;

namespace TechAssess.ScrapingService.Factories
{
    public class ScraperFactory : IScraperFactory
    {
        public AbstractScraper GetScraper(Source source)
        {
            return source switch
            {
                Source.OLD => new OLDScraper(),
                Source.WB => new WBScraper(),
                Source.OFAC => new OFACScraper(),
                _ => throw new ArgumentException("Scraper for source not found")
            };
        }
    }
}
