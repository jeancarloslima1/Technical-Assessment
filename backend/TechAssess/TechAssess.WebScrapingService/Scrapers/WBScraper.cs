using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechAssess.ScrapingService.Models;

namespace TechAssess.ScrapingService.Scrapers
{
    public class WBScraper : IScraper
    {
        public List<ScrapeData> Scrap(string supplierName)
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("user-agent=Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36");
            options.AddArguments("disable-gpu");
            options.AddArguments("window-size=1920,1080");
            options.AddArguments("no-sandbox");
            options.AddArguments("disable-dev-shm-usage");
            options.AddArguments("disable-infobars");
            options.AddArguments("start-maximized");
            using var driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://projects.worldbank.org/en/projects-operations/procurement/debarred-firms");

            //Wait for initial table to load
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(200)
            };
            var table = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.k-grid-content.k-auto-scrollable > table[role='grid']")));

            //Search supplier
            driver.FindElement(By.Id("category")).SendKeys(supplierName);

            //Extract data from table
            var rows = table.FindElements(By.TagName("tr"));
            var tableData = new List<ScrapeData>();

            for (int i = 0; i < rows.Count; i++)
            {
                var cells = rows[i].FindElements(By.TagName("td"));

                var tableRow = new WBData
                {
                    FirmName = cells[0].Text,
                    Address = cells[2].Text,
                    Country = cells[3].Text,
                    FromDate = cells[4].Text,
                    ToDate = cells[5].Text,
                    Grounds = cells[6].Text
                };

                tableData.Add(tableRow);
            }
            return tableData;
        }
    }
}
