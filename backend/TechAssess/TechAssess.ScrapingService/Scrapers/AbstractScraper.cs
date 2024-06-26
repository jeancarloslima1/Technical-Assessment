using System.Diagnostics;
using TechAssess.ScrapingService.Models;

namespace TechAssess.ScrapingService.Scrapers
{
    public abstract class AbstractScraper
    {
        public abstract List<ScrapeData> Scrap(string supplierName);
        protected void KillChromeDriverProcesses()
        {
            var chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
            foreach (var process in chromeDriverProcesses)
            {
                process.Kill();
            }

            var chromeProcesses = Process.GetProcessesByName("chrome");
            foreach (var process in chromeProcesses)
            {
                if (process.MainWindowTitle == string.Empty)
                {
                    process.Kill();
                }
            }
        }
    }
}
