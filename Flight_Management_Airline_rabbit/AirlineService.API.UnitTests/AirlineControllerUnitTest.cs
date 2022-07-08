using AirlineService.Controllers;
using AirlineService.Interfaces;
using AirlineService.Models;
using AirlineService.Services;
using AirlineService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AirlineService.API.UnitTests
{
    public class AirlineControllerUnitTest
    {
        //[Fact]
        //public void TestAddAirline()
        //{
        //    //Arrange
        //    var dbContext = DbContextMocker.GetFlightManagementDbContext();
        //    IAirlineManagementRepository airlineManagementRepository = new AirlineManagementRepository(dbContext);
        //    var controller = new AirlineController(airlineManagementRepository, new NullLogger<AirlineController>());

        //    //Act
        //    var request = new AirlineDetails
        //    {
        //        AirlineAddress = "Delhi, India,",
        //        AirlineContact = "+91 9876543122",
        //        AirlineDescription = "Indigo Ready to fly",
        //        AirlineLogo = "",
        //        AirlineName = "Indigo",
        //        IsBlocked = false
        //    };
        //    var response = controller.RegisterAirline(request);
        //    //var value = response;

        //    dbContext.Dispose();
        //    //Assert
        //    ObjectResult objectResponse = Assert.IsType<ObjectResult>(response);
        //    Assert.Equal(201, objectResponse.StatusCode);

        //}

        [Fact]
        public void TestAddAirlineSchedule()
        {
            //Arrange
            var dbContext = DbContextMocker.GetFlightManagementDbContext();
            IAirlineManagementRepository airlineManagementRepository = new AirlineManagementRepository(dbContext,null);
            var controller = new AirlineController(airlineManagementRepository, new NullLogger<AirlineController>());

            //Act
            var request = new AirlineInventorySchedule
            {
                FlightNumber = "IG-2244",
                AirLineId = 1,
                FlightDayScheduleId = 0,
                InstrumentId = 1,
                BusinessSeatsNo = 14,
                RegularSeatsNo = 14,
                TicketCost = Convert.ToDecimal(2400.44),
                NoOfRows = 32,
                MealPlanId = 1,
                SourceLocationId = 2,
                DestinationLocationId = 1,
                StartDateTime = Convert.ToDateTime("2022-07-01T16:00:00.741Z"),
                EndDateTime = Convert.ToDateTime("2022-07-30T16:00:00.741Z"),
                Monday = false,
                Tuesday = false,
                Wednesday = false,
                Thursday = false,
                Friday = true,
                Saturday = false,
                Sunday = true
            };

            var response = controller.AddAirlineInventory(request);

            ObjectResult objectResponse = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, objectResponse.StatusCode);
        }
    }
}
