using LoginService.Controllers;
using LoginService.Interface;
using LoginService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LoginService.API.UnitTests
{
    public class LoginControllerUnitTest
    {
        [Fact]
        public void TestUserLogin()
        {
            //Arrange
            var dbContext = DbContextMocker.GetFlightManagementDbContext();
            Dictionary<string, string> inMemorySettings = new Dictionary<string, string>();
            inMemorySettings.Add("JWT:Key", "This is My super Secret key for jwt");

            //{ "JWT:Key", ""};
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            ILoginManagementRepository loginManagementRepository = new LoginManagementRepository(configuration, dbContext);
            var controller = new LoginController(loginManagementRepository, new NullLogger<LoginController>());

                //Act
            var response = controller.AuthenticateAdmin(new ViewModel.Login { UserName = "test@test.com", Password = "12345678" });

            //Assert
            ObjectResult objResponse = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, objResponse.StatusCode);
;        }
    }
}
