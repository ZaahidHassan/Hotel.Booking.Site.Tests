using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Hotel.Booking.Site.Tests.Helpers
{
    internal class TestBase
    {
        public IWebDriver driver = new ChromeDriver();
        const string url = "http://hotel-test.equalexperts.io/";
        public ExtentTest? testReport;
        public ExtentHtmlReporter? htmlReporter;
        public ExtentReports? extent;
        readonly string reportPath = Directory.GetCurrentDirectory().ToString() + "\\TestReport";

        /// <summary>
        /// Setup method to setup the web browser
        /// </summary>
        public void Setup()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(1500);
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.Id("checkout")));
            //Setup the ExtentReport
            SetupReport();
        }

        /// <summary>
        /// Setup method to setup the ExtentReport
        /// </summary>
        public void SetupReport()
        {
            //Current issue with extent report where name is always deaulted to index.html https://github.com/extent-framework/extentreports-csharp/issues/132
            htmlReporter = new ExtentHtmlReporter(reportPath + "/" + "Hotel_Booking_Test_Report" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Brower", "Chrome");
            extent.AddSystemInfo("OS", "Windows");
        }

        /// <summary>
        /// Method to take screenshots after each test
        /// </summary>
        /// <param name="screenshotName">Name of the screenshot that is being taken</param>
        public void TakeScreenshot(string screenshotName)
        {
            Screenshot screenshot;
            Thread.Sleep(1000);
            screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(reportPath + "/" + screenshotName);
        }

        /// <summary>
        /// Teardown method to terminate the webdriver instance and erase previous data on the Extent Report
        /// </summary>
        public void TearDown()
        {
            driver.Quit();
            extent?.Flush();
        }
    }
}
