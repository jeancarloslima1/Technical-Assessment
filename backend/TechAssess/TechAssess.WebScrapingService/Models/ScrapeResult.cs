namespace TechAssess.ScrapingService.Models
{
    public class ScrapeResult
    {
        public string Source { get; set; } = string.Empty;
        public int Hits { get; set; }
        public IEnumerable<ScrapeData> ScrapeData { get; set; } = Enumerable.Empty<ScrapeData>();
    }
}
