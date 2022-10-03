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

            //Save all the elements that will be interacted with into variables to be used later
            FirstNameField = driver.FindElement(By.Id("firstname"));
            LastNameField = driver.FindElement(By.Id("lastname"));
            TotalPriceField = driver.FindElement(By.Id("totalprice"));
            DepositPaidDropDown = driver.FindElement(By.Id("depositpaid"));
            CheckInDatePicker = driver.FindElement(By.Id("checkin"));
            CheckOutDatePicker = driver.FindElement(By.Id("checkout"));
            SaveButton = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div[7]/input"));
        }

        /// <summary>
        /// Method to add the first name of the guest to the booking form
        /// </summary>
        /// <param name="firstName">The first name of the guest</param>
        public void AddFirstName(string firstName)
        {
            FirstNameField.Click();
            FirstNameField.SendKeys(firstName);
        }

        /// <summary>
        /// Method to add the last name of the guest to the booking form
        /// </summary>
        /// <param name="lastName">The last name of the guest</param>
        public void AddLastName(string lastName)
        {
            LastNameField.Click();
            LastNameField.SendKeys(lastName);
        }

        /// <summary>
        /// Method to add the price of the booking
        /// </summary>
        /// <param name="totalPrice">The price of the booking</param>
        public void AddTotalPrice(string totalPrice)
        {
            TotalPriceField.Click();
            TotalPriceField.SendKeys(totalPrice);
        }

        /// <summary>
        /// Method to add whether a deposit has been paid or not
        /// </summary>
        /// <param name="depositPaid">Whether a deposit has been paid or not</param>
        public void AddDepositPaid(string depositPaid)
        {
            var selectElement = new SelectElement(DepositPaidDropDown);
            selectElement.SelectByText(depositPaid);
        }

        /// <summary>
        /// Method to add the check-in date of the booking
        /// </summary>
        /// <param name="xPathCheckInDate">The check-in date of the booking</param>
        public void CaptureCheckInDate(string xPathCheckInDate)
        {
            CheckInDatePicker.Click();
            var checkInDate = driver.FindElement(By.XPath(xPathCheckInDate));
            checkInDate.Click();
        }

        /// <summary>
        /// Method to add the check-out date of the booking
        /// </summary>
        /// <param name="xPathCheckOutDate">The check-out date of the booking</param>
        public void CaptureCheckOutDate(string xPathCheckOutDate)
        {
            CheckOutDatePicker.Click();
            var checkOutDate = driver.FindElement(By.XPath(xPathCheckOutDate));
            checkOutDate.Click();
        }

        /// <summary>
        /// Method to click the save button in order to save the booking
        /// </summary>
        public void SaveBooking()
        {
            SaveButton.Click();
        }

        /// <summary>
        /// Method to click the delete button in order to delete a booking, specified by the bookingId
        /// </summary>
        /// <param name="bookingId">The bookingId that will be deleted</param>
        public void DeleteBooking(string bookingId)
        {
            var deleteButton = driver.FindElement(By.XPath("/html/body/div/div[2]/div[@id=" + bookingId + "]/div[7]/input"));
            deleteButton.Click();
        }
    }
}