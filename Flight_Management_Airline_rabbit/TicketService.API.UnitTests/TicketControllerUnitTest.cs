using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using TicketService.Controllers;
using TicketService.Interface;
using TicketService.Service;
using Xunit;

namespace TicketService.API.UnitTests
{
    public class TicketControllerUnitTest
    {
        [Fact]
        public void TestGetTicketFromPnr()
        {
            //Arrange
            var dbContext = DbContextMocker.GetFlightManagementDbContext();
            ITicketManagementRepository ticketManagementRepository = new TicketManagementRepository(dbContext);

            var controller = new TicketController(ticketManagementRepository, new NullLogger<TicketController>());
            string pnrNumber = "6792093764";

            //Act
            var response = controller.GetTicketDetailsFromPNR(pnrNumber);

            //Assert
            ObjectResult objResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, objResult.StatusCode);
        }
    }
}
