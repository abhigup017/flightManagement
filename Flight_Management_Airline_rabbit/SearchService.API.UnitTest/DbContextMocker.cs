using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SearchService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchService.API.UnitTest
{
    public static class DbContextMocker
    {
        public static FlightManagementContext GetFlightManagementDbContext()
        {
            //create option for db context instance
            var options = new DbContextOptionsBuilder<FlightManagementContext>()
                .UseInMemoryDatabase(databaseName: "FlightManagement")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            //create instance of db context
            var dbContext = new FlightManagementContext(options);

            //add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
