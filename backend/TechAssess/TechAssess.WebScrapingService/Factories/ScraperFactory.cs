using TechAssess.ScrapingService.Enums;
using TechAssess.ScrapingService.Scrapers;

namespace TechAssess.ScrapingService.Factories
{
    public class ScraperFactory : IScraperFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ScraperFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IScraper GetScraper(Source source)
        {
            return source switch
            {
                Source.OLD => _serviceProvider.GetService<OLDScraper>() ?? throw new InvalidOperationException("OLDScraper not registered"),
                Source.WB => _serviceProvider.GetService<WBScraper>() ?? throw new InvalidOperationException("WBScraper not registered"),
                Source.OFAC => _serviceProvider.GetService<OFACScraper>() ?? throw new InvalidOperationException("OFACScraper not registered"),
                _ => throw new ArgumentException("Scraper for source not found")
            };
        }
    }
}
