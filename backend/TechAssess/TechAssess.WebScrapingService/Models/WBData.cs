﻿namespace TechAssess.ScrapingService.Models
{
    public class WBData : ScrapeData
    {
        public string FirmName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public string Grounds { get; set; } = string.Empty;
    }
}
