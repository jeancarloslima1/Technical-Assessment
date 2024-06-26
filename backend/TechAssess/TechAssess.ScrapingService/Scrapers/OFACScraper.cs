using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;
using TechAssess.ScrapingService.Models;

namespace TechAssess.ScrapingService.Scrapers
{
    public class OFACScraper : AbstractScraper
    {
        public override List<ScrapeData> Scrap(string supplierName)
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
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-software-rasterizer");
            options.AddArgument("--disable-notifications");

            ChromeDriver? driver = null;
            var scrapeResponse = new List<ScrapeData>();
            try
            {
                driver = new ChromeDriver(options);
                driver.Navigate().GoToUrl("https://sanctionssearch.ofac.treas.gov/");

                //Wait for page to load
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(200)
                };
                var nameTextBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ctl00_MainContent_txtLastName")));

                //Search supplier
                nameTextBox.SendKeys(supplierName);
                driver.FindElement(By.Id("ctl00_MainContent_btnSearch")).Click();
                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                //Extract data from table
                try
                {
                    var table = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("gvSearchResults")));
                    var rows = table.FindElements(By.TagName("tr"));
                    var tableData = new List<ScrapeData>();

                    for (int i = 0; i < rows.Count; i++)
                    {
                        var cells = rows[i].FindElements(By.TagName("td"));

                        var tableRow = new OFACData
                        {
                            Name = cells[0].FindElement(By.TagName("a")).Text,
                            Address = cells[1].Text,
                            Type = cells[2].Text,
                            Programs = cells[3].Text,
                            List = cells[4].Text,
                            Score = cells[5].Text,
                        };

                        tableData.Add(tableRow);
                    }
                    scrapeResponse = tableData;
                }
                catch (WebDriverTimeoutException)
                {

                    try
                    {
                        var noResultsParagraph = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ctl00_MainContent_pnlMessage")));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        throw new Exception("Page didn't load");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                driver?.Quit();
                driver?.Dispose(); 
                KillChromeDriverProcesses();
            }

            return scrapeResponse;
        }
    }
}
