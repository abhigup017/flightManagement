using LoginService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginService.API.UnitTests
{
    public static class DbContextExtensions
    {
        public static void Seed(this FlightManagementContext dbContext)
        {
            dbContext.Users.Add(new User
            {
                UserId = 1,
                FirstName = "Abhishek",
                LastName = "Gupta",
                GenderId = 1,
                EmailId = "test@test.com",
                RoleId = 1,
                UserName = "test@test.com",
                Password = "12345678"
            });

            dbContext.Users.Add(new User
            {
                UserId = 2,
                FirstName = "Ankita",
                LastName = "Gupta",
                GenderId = 1,
                EmailId = "test@test.com",
                RoleId = 1,
                UserName = "testankita@test.com",
                Password = "12345678"
            });

            dbContext.SaveChanges();
        }
    }
}
