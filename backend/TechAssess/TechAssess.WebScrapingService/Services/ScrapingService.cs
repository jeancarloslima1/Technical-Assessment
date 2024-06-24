using System.Threading.Tasks;
using TechAssess.ScrapingService.Enums;
using TechAssess.ScrapingService.Factories;
using TechAssess.ScrapingService.Models;
using TechAssess.ScrapingService.Scrapers;
using TechAssess.WebScrapingService.Services;

namespace TechAssess.ScrapingService.Services
{
    public class ScrapingService : IScrapingService
    {
        private readonly IScraperFactory _scraperFactory;

        public ScrapingService(IScraperFactory scraperFactory)
        {
            _scraperFactory = scraperFactory;
        }

        public IEnumerable<ScrapeResult> Scrap(string supplierName, IEnumerable<Source> sources)
        {
            var results = new List<ScrapeResult>();

            foreach (var source in sources)
            {
                try
                {
                    var scraper = _scraperFactory.GetScraper(source);
                    var scrapeData = scraper.Scrap(supplierName);
                    var sourceName = source switch
                    {
                        Source.OLD => "Offshore Leaks Database",
                        Source.WB => "The World Bank",
                        Source.OFAC => "OFAC",
                        _ => throw new ArgumentException("Unregistered source")
                    };
                    results.Add(new ScrapeResult
                    {
                        Source = sourceName,
                        Hits = scrapeData.Count,
                        ScrapeData = scrapeData
                    });
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error scraping from {Enum.GetName(typeof(Source), source)}: {ex.Message}");
                }
            }

            return results;

        }
    }
}
