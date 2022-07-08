using AirlineService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineService.API.UnitTests
{
    public static class DbContextExtensions
    {
        public static void Seed(this FlightManagementContext dbContext)
        {
            dbContext.Airlines.Add(new Airline
            {
                AirLineId = 4,
                AirlineLogo = "",
                AirlineName = "Indigo",
                AirlineContact = "+91 123456789",
                AirlineAddress = "Delhi, India",
                AirlineDescription = "Indigo Ready to fly",
                IsBlocked = false
            });

            dbContext.Airlines.Add(new Airline
            {
                AirLineId = 5,
                AirlineLogo = "",
                AirlineName = "Spice Jet",
                AirlineContact = "+91 123456789",
                AirlineAddress = "Noida, India",
                AirlineDescription = "SpiceJet Ready to fly",
                IsBlocked = false
            });

            dbContext.Flightschedules.Add(new Flightschedule
            {
                FlightId = 4,
                FlightNumber = "IG-1234",
                AirLineId = 40,
                FlightDayScheduleId = 2,
                InstrumentId = 1,
                BusinessSeatsNo = 14,
                RegularSeatsNo = 14,
                VacantBusinessSeats = 10,
                VacantRegularSeats = 8,
                TicketCost = Convert.ToDecimal(2400.24),
                NoOfRows = 32,
                MealPlanId = 1
            });

            dbContext.Flightdaysschedules.Add(new Flightdaysschedule
            {
                FlightDayScheduleId = 2,
                SourceLocationId = 1,
                DestinationLocationId = 2,
                StartDateTime = Convert.ToDateTime("2022-07-04T18:00:00.740Z"),
                EndDateTime = Convert.ToDateTime("2022-07-04T18:00:00.740Z"),
            });

            dbContext.SaveChanges();
        }
    }
}
