using NUnit.Framework;
using Hotel.Booking.Site.Tests.Helpers;
using FluentAssertions;
using AventStack.ExtentReports;

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
            string addBookingScreenshotName = "Booking_Added_" + DateTime.Now.ToString("dd'-'MM'-'yyyy'T'HH'-'mm'-'ss") + ".png";

            helper.Setup();
            helper.testReport = helper.extent?.CreateTest("GIVEN_booking_does_not_exist_WHEN_valid_bookings_added_THEN_booking_added_to_list", "Ensure that a new booking can be made");

            //Act
            helper.AddBooking(firstName, lastName, price, deposit, checkInDate, checkOutDate);
            helper.TakeScreenshot(addBookingScreenshotName);

            int bookingId = await helper.GetBookingId();
            string name = await helper.GetBookingName(bookingId.ToString());

            MediaEntityModelProvider mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath(addBookingScreenshotName).Build();

            if (name == firstName)
            {
                //Assert
                Assert.That(name, Is.EqualTo(firstName));
                helper.testReport?.Log(Status.Pass, "Booking added correctly", mediaModel);
            }
            else
            {
                helper.testReport?.Log(Status.Fail, "Booking not added correctly", mediaModel);
            }
        }

        [Test, Order(2), Description("Ensure that an existing booking can be deleted")]
        public async Task GIVEN_existing_booking_WHEN_booking_deleted_THEN_booking_is_removed()
        {
            //Arrange
            int bookingIdToDelete = await helper.GetBookingId();
            string deleteBookingScreenshotName = "Booking_Deleted_" + DateTime.Now.ToString("dd'-'MM'-'yyyy'T'HH'-'mm'-'ss") + ".png";
            helper.testReport = helper.extent?.CreateTest("GIVEN_existing_booking_WHEN_booking_deleted_THEN_booking_is_removed", "Ensure that an existing booking can be deleted");

            //Act
            helper.DeleteBooking(bookingIdToDelete.ToString());
            helper.TakeScreenshot(deleteBookingScreenshotName);

            MediaEntityModelProvider mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath(deleteBookingScreenshotName).Build();

            var bookingIdCheck = await helper.GetBookingId();

            if (bookingIdCheck != bookingIdToDelete)
            {
                //Assert
                bookingIdCheck.Should().NotBe(bookingIdToDelete);
                helper.testReport?.Log(Status.Pass, "Booking removed correctly", mediaModel);
            }
            else
            {
                helper.testReport?.Log(Status.Fail, "Booking not removed correctly", mediaModel);
            }
            helper.TearDown();
        }
    }
}