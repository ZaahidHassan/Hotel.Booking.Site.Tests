using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Hotel.Booking.Site.Tests.Helpers
{
    internal class FormHelper
    {
        private IWebDriver driver;
        public IWebElement FirstNameField { get; set; }
        public IWebElement LastNameField { get; set; }
        public IWebElement TotalPriceField { get; set; }
        public IWebElement DepositPaidDropDown { get; set; }
        public IWebElement CheckInDatePicker { get; set; }
        public IWebElement CheckOutDatePicker { get; set; }
        public IWebElement SaveButton { get; set; }
        public IWebElement? DeleteButton { get; set; } 

        public FormHelper(IWebDriver driver)
        {
            this.driver = driver;

            FirstNameField = driver.FindElement(By.Id("firstname"));
            LastNameField = driver.FindElement(By.Id("lastname"));
            TotalPriceField = driver.FindElement(By.Id("totalprice"));
            DepositPaidDropDown = driver.FindElement(By.Id("depositpaid"));
            CheckInDatePicker = driver.FindElement(By.Id("checkin"));
            CheckOutDatePicker = driver.FindElement(By.Id("checkout"));
            SaveButton = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div[7]/input"));
        }

        public void AddFirstName(string firstName)
        {
            FirstNameField.Click();
            FirstNameField.SendKeys(firstName);
        }

        public void AddLastName(string lastName)
        {
            LastNameField.Click();
            LastNameField.SendKeys(lastName);
        }

        public void AddTotalPrice(string totalPrice)
        {
            TotalPriceField.Click();
            TotalPriceField.SendKeys(totalPrice);
        }

        public void AddDepositPaid(string depositPaid)
        {
            var selectElement = new SelectElement(DepositPaidDropDown);
            selectElement.SelectByText(depositPaid);
        }

        public void CaptureCheckInDate(string xPathCheckInDate)
        {
            CheckInDatePicker.Click();
            var checkInDate = driver.FindElement(By.XPath(xPathCheckInDate));
            checkInDate.Click();
        }

        public void CaptureCheckOutDate(string xPathCheckOutDate)
        {
            CheckOutDatePicker.Click();
            var checkOutDate = driver.FindElement(By.XPath(xPathCheckOutDate));
            checkOutDate.Click();
        }

        public void SaveBooking()
        {
            SaveButton.Click();
        }

        public void DeleteBooking(string bookingId)
        {
            var deleteButton = driver.FindElement(By.XPath("/html/body/div/div[2]/div[@id=" + bookingId + "]/div[7]/input"));
            deleteButton.Click();
        }
    }
}
