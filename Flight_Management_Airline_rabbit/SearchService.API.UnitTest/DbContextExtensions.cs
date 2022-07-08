using SearchService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchService.API.UnitTest
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
                AirLineId = 4,
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
                EndDateTime = Convert.ToDateTime("2022-07-04T18:00:00.740Z")
            });

            dbContext.Mealplans.Add(new Mealplan
            {
                MealPlanId = 1,
                MealPlanType = "Veg"
            });

            dbContext.Mealplans.Add(new Mealplan
            {
                MealPlanId = 2,
                MealPlanType = "Non-Veg"
            });

            dbContext.Mealplans.Add(new Mealplan
            {
                MealPlanId = 3,
                MealPlanType = "Veg & Non-Veg"
            });

            dbContext.Mealplans.Add(new Mealplan
            {
                MealPlanId = 4,
                MealPlanType = "None"
            });

            dbContext.SaveChanges();
        }
    }
}
