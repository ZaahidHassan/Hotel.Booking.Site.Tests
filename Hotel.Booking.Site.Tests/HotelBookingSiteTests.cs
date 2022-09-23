using NUnit.Framework;
using Hotel.Booking.Site.Tests.Helpers;
using FluentAssertions;

namespace Hotel.Booking.Site.Tests
{
    [TestFixture(Description = "Tests for adding a booking and deleting a booking")]
    internal class HotelBookingSiteTests
    {
        TestMethodHelper helper = new();

        [Test, Order(1), Description("Ensure that a new booking can be made")]
        public async Task GIVEN_booking_does_not_exist_WHEN_valid_bookings_added_THEN_booking_added_to_list()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Doe";
            string price = "100";
            string deposit = "false";
            string checkInDate = "/html/body/div[2]/table/tbody/tr[4]/td[1]/a";
            string checkOutDate = "/html/body/div[2]/table/tbody/tr[4]/td[7]/a";
            helper.Setup();

            //Act
            helper.AddBooking(firstName, lastName, price, deposit, checkInDate, checkOutDate);
            helper.TakeScreenshot("BookingAdded.png");

            //Assert
            int bookingId = await helper.GetBookingId();
            string name = await helper.GetBookingName(bookingId.ToString());
            Assert.That(name, Is.EqualTo(firstName));
        }

        [Test, Order(2), Description("Ensure that an existing booking can be deleted")]
        public async Task GIVEN_existing_booking_WHEN_booking_deleted_THEN_booking_is_removed()
        {
            //Arrange
            int bookingIdToDelete = await helper.GetBookingId();

            //Act
            helper.DeleteBooking(bookingIdToDelete.ToString());
            helper.TakeScreenshot("BookingDeleted.png");

            //Assert
            var bookingIdCheck = await helper.GetBookingId();
            bookingIdCheck.Should().NotBe(bookingIdToDelete);
            helper.TearDown();
        }
    }
}