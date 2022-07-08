using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using SearchService.Controllers;
using SearchService.Interface;
using SearchService.Service;
using SearchService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SearchService.API.UnitTest
{
    public class SearchControllerUnitTests
    {
        [Fact]
        public void TestFlightSearch()
        {
            //Arrange
            var dbContext = DbContextMocker.GetFlightManagementDbContext();
            ISearchManagementRepository searchManagementRepository = new SearchManagementRepository(dbContext);

            var controller = new SearchController(searchManagementRepository, new NullLogger<SearchController>());

            FlightSearchRequest flightSearchRequest = new FlightSearchRequest();
            flightSearchRequest.OnwardTripRequest = new FlightSearchParamaters
            {
                SourceId = 1,
                DestinationId = 2,
                TravelDateTime = Convert.ToDateTime("2022-07-04T18:00:00.740Z"),
                IsTimeBasedSearch = false
            };
            flightSearchRequest.RoundTripRequest = null;

            //Act
            var response = controller.SearchFlightsForBooking(flightSearchRequest);

            //Assert
            ObjectResult objResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, objResult.StatusCode);
           
        }
    }
}
