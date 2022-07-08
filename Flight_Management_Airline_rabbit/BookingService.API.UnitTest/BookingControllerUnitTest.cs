using BookingService.Controllers;
using BookingService.Interfaces;
using BookingService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookingService.API.UnitTest
{
    public class BookingControllerUnitTest
    {
        [Fact]

        public void TestGetBookedTicketsHistory()
        {
            //Arrange
            var dbContext = DbContextMocker.GetFlightManagementDbContext();
            IBookingManagementRepository bookingManagementRepository = new BookingManagementRepository(dbContext, null);
            var controller = new BookingController(bookingManagementRepository, new NullLogger<BookingController>());

            //Act
            string request = "test@test.com";

            var response = controller.GetBookedTicketsHistory(request);
            dbContext.Dispose();
            //Assert
            ObjectResult objectResponse = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, objectResponse.StatusCode);
        }

        [Fact]
        public void TestCancelBooking()
        {
            //Arrange
            var dbContext = DbContextMocker.GetFlightManagementDbContext();
            IBookingManagementRepository bookingManagementRepository = new BookingManagementRepository(dbContext, null);
            var controller = new BookingController(bookingManagementRepository, new NullLogger<BookingController>());

            //Act
            string request = "6792093764";

            var response = controller.CancelBooking(request);
            dbContext.Dispose();

            //Assert
            ObjectResult objResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, objResult.StatusCode);
        }
    }
}
