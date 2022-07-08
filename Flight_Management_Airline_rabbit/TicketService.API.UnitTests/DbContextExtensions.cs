using System;
using System.Collections.Generic;
using System.Text;
using TicketService.Models;

namespace TicketService.API.UnitTests
{
    public static class DbContextExtensions
    {
        public static void Seed(this FlightManagementContext dbContext)
        {
            dbContext.Airlines.Add(new Airline
            {
                AirLineId = 6,
                AirlineLogo = "",
                AirlineName = "Indigo",
                AirlineContact = "+91 123456789",
                AirlineAddress = "Delhi, India",
                AirlineDescription = "Indigo Ready to fly",
                IsBlocked = false
            });

            dbContext.Airlines.Add(new Airline
            {
                AirLineId = 7,
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
                EndDateTime = Convert.ToDateTime("2022-07-04T18:00:00.740Z")
            });
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

            dbContext.Bookings.Add(new Booking
            {
                BookingId = 4,
                FlightId = 4,
                CustomerEmailId = "test@test.com",
                NoOfSeats = 1,
                MealPlanId = 1,
                Pnrnumber = "6792093764",
                TravelDate = Convert.ToDateTime("2022-07-14T18:00:00.740Z"),
                BookedOn = Convert.ToDateTime("2022-07-04T18:00:00.740Z"),
                TotalCost = Convert.ToDecimal(2800.44),
                IsCancelled = false,
                CustomerName = "Abhishek"
            });

            dbContext.Bookings.Add(new Booking
            {
                BookingId = 5,
                FlightId = 4,
                CustomerEmailId = "test2@test.com",
                NoOfSeats = 2,
                MealPlanId = 2,
                Pnrnumber = "6792092974",
                TravelDate = Convert.ToDateTime("2022-07-20T18:00:00.740Z"),
                BookedOn = Convert.ToDateTime("2022-07-12T18:00:00.740Z"),
                TotalCost = Convert.ToDecimal(4000),
                IsCancelled = false,
                CustomerName = "Anand"
            });

            dbContext.Bookingpassengers.Add(new Bookingpassenger
            {
                PassengerId = 4,
                BookingId = 4,
                PassengerName = "Abhishek",
                GenderId = 1,
                PassengerAge = 26,
                SeatNo = "1",
                IsBusinessSeat = true,
                IsRegularSeat = false,

            });

            dbContext.Bookingpassengers.Add(new Bookingpassenger
            {
                PassengerId = 5,
                BookingId = 4,
                PassengerName = "Ankita",
                GenderId = 2,
                PassengerAge = 35,
                SeatNo = "2",
                IsBusinessSeat = true,
                IsRegularSeat = false,

            });

            dbContext.Bookingpassengers.Add(new Bookingpassenger
            {
                PassengerId = 6,
                BookingId = 5,
                PassengerName = "A.K Gupta",
                GenderId = 1,
                PassengerAge = 65,
                SeatNo = "8",
                IsBusinessSeat = false,
                IsRegularSeat = true,

            });

            dbContext.Bookingpassengers.Add(new Bookingpassenger
            {
                PassengerId = 7,
                BookingId = 5,
                PassengerName = "Renu Gupta",
                GenderId = 2,
                PassengerAge = 60,
                SeatNo = "9",
                IsBusinessSeat = false,
                IsRegularSeat = true,

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

            dbContext.Gendertypes.Add(new Gendertype
            {
                GenderId = 1,
                GenderValue = "Male"
            }) ;

            dbContext.Gendertypes.Add(new Gendertype
            {
                GenderId = 2,
                GenderValue = "Female"
            });

            dbContext.Gendertypes.Add(new Gendertype
            {
                GenderId = 3,
                GenderValue = "Others"
            });

            dbContext.SaveChanges();
        }
    }
}
