using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechAssess.ScrapingService.Models;

namespace TechAssess.ScrapingService.Scrapers
{
    public class OLDScraper : IScraper
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

            driver.Navigate().GoToUrl("https://offshoreleaks.icij.org/");

            //Wait and accept TOS
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("accept"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='submit' and contains(text(), 'Submit')]"))).Click();

            //Search supplier
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("q"))).SendKeys(supplierName);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='submit' and contains(text(), 'Search')]"))).Click();

            //Extract data from table
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            try
            {
                var table = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".search__results__content table.search__results__table")));
                var rows = table.FindElements(By.TagName("tr"));
                var tableData = new List<ScrapeData>();

                for (int i = 1; i < rows.Count; i++)
                {
                    var cells = rows[i].FindElements(By.TagName("td"));
                    var tableRow = new OLDData
                    {
                        Entity = cells[0].FindElement(By.TagName("a")).Text,
                        Jurisdiction = cells[1].Text,
                        LinkedTo = cells[2].Text,
                        DataFrom = cells[3].FindElement(By.TagName("a")).Text
                    };
                    tableData.Add(tableRow);
                }
                return tableData;
            }
            catch (WebDriverTimeoutException)
            {

                try
                {
                    var noResultsParagraph = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".search__results__content p.lead")));
                }
                catch (WebDriverTimeoutException)
                {
                    //Bot detected
                    throw new Exception("Cloudfront 403: request could not be satisfied");
                }
            }


            return new List<ScrapeData>();
        }
    }
}
