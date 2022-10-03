using System.Text.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Hotel.Booking.Site.Tests.Helpers
{
    internal class TestMethodHelper : TestBase
    {
        const string bookingUrl = "http://hotel-test.equalexperts.io/booking/";
        public HttpClient client = new();

        /// <summary>
        /// Method to add a hotel booking
        /// </summary>
        /// <param name="firstName">The first name of the guest for whom you are adding the booking</param>
        /// <param name="lastName">The last name of the guest for whom you are adding the booking</param>
        /// <param name="totalPrice">The price of the booking</param>
        /// <param name="depositPaid">Whether a deposit has been made or not</param>
        /// <param name="checkInDate">The check-in date of the guest</param>
        /// <param name="checkOutDate">The check-out dateof the guest</param>
        public void AddBooking(string firstName, string lastName, string totalPrice, string depositPaid, string checkInDate, string checkOutDate)
        {
            FormHelper form = new(driver); 

            //Add all the required information to the form and click save
            form.AddFirstName(firstName);
            form.AddLastName(lastName);
            form.AddTotalPrice(totalPrice);
            form.AddDepositPaid(depositPaid);
            form.CaptureCheckInDate(checkInDate);
            form.CaptureCheckOutDate(checkOutDate);
            form.SaveBooking();
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Method to retrieve a booking name
        /// </summary>
        /// <param name="bookingId">The bookingId that is used to retrieve the guest's first name</param>
        /// <returns>The first name of the guest corresponding to the bookingId that is passed in</returns>
        public async Task<string> GetBookingName(string bookingId)
        {
            HttpRequestMessage request = new(HttpMethod.Get, bookingUrl + bookingId);
            request.Headers.Add("Accept", "*/*");

            HttpResponseMessage response = await client.SendAsync(request);
            JsonDocument bookings = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            
            var singleBookingId = bookings.RootElement;
            
            string firstName = singleBookingId.GetProperty("firstname").ToString();

            return firstName;
        }

        /// <summary>
        /// Method to retrieve a BookingId
        /// </summary>
        /// <returns>The bookingId of the last boookingId that was added</returns>
        public async Task<int> GetBookingId()
        {
            HttpRequestMessage request = new(HttpMethod.Get, bookingUrl);
            HttpResponseMessage response = await client.SendAsync(request);
            JsonDocument bookings = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            List<int> bookingIds = new();

            if (bookings.RootElement.GetArrayLength() == 0)
            {
                //If no bookings exist, return 0
                return 0;
            }
            else
            {
                //Extract the bookingIds
                for (int i = 0; i < bookings.RootElement.GetArrayLength(); i++)
                {
                    var singleBookingId = bookings.RootElement[i];
                    var bookingId = singleBookingId.GetProperty("bookingid");
                    bookingIds.Add(int.Parse(bookingId.ToString()));
                }
                //return the last bookingId
                return bookingIds.Last();
            }
        }

        /// <summary>
        /// Method to delete an existing booking
        /// </summary>
        /// <param name="bookingId">The bookingId that is to be deleted</param>
        public void DeleteBooking(string bookingId)
        {
            FormHelper form = new(driver);
            
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.Id(bookingId)));
            
            form?.DeleteBooking(bookingId);
            Thread.Sleep(3000);
        }
    }
}
