namespace TechAssess.ScrapingService.Models
{
    public class OFACData : ScrapeData
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Programs { get; set; } = string.Empty;
        public string List { get; set; } = string.Empty;
        public string Score { get; set; } = string.Empty;
    }
}
