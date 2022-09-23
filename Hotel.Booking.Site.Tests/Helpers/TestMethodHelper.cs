using System.Text.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Hotel.Booking.Site.Tests.Helpers
{
    internal class TestMethodHelper : TestBase
    {
        const string bookingUrl = "http://hotel-test.equalexperts.io/booking/";
        public HttpClient client = new();

        public void AddBooking(string firstName, string lastName, string totalPrice, string depositPaid, string checkInDate, string checkOutDate)
        {
            FormHelper form = new(driver); 

            form.AddFirstName(firstName);
            form.AddLastName(lastName);
            form.AddTotalPrice(totalPrice);
            form.AddDepositPaid(depositPaid);
            form.CaptureCheckInDate(checkInDate);
            form.CaptureCheckOutDate(checkOutDate);
            form.SaveBooking();
            Thread.Sleep(3000);
        }

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

        public async Task<int> GetBookingId()
        {
            HttpRequestMessage request = new(HttpMethod.Get, bookingUrl);
            HttpResponseMessage response = await client.SendAsync(request);
            JsonDocument bookings = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            List<int> bookingIds = new();

            if (bookings.RootElement.GetArrayLength() == 0)
            {
                return 0;
            }
            else
            {
                for (int i = 0; i < bookings.RootElement.GetArrayLength(); i++)
                {
                    var singleBookingId = bookings.RootElement[i];
                    var bookingId = singleBookingId.GetProperty("bookingid");
                    bookingIds.Add(int.Parse(bookingId.ToString()));
                }
                return bookingIds.Last();
            }
        }

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
