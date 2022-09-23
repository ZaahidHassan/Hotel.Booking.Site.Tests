using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Hotel.Booking.Site.Tests.Helpers
{
    internal class TestBase
    {
        public IWebDriver driver = new ChromeDriver();
        const string url = "http://hotel-test.equalexperts.io/";

        public void Setup()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(1500);
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.Id("checkout")));
        }

        public void TakeScreenshot(string screenshotName)
        {
            Screenshot screenshot;
            Thread.Sleep(1000);
            screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(Directory.GetCurrentDirectory() + "/" + screenshotName);
        }

        public void TearDown()
        {
            driver.Quit();
        }
    }
}
