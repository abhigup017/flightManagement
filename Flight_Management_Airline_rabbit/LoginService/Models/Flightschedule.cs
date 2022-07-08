using System;
using System.Collections.Generic;

#nullable disable

namespace LoginService.Models
{
    public partial class Flightschedule
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public int AirLineId { get; set; }
        public int FlightDayScheduleId { get; set; }
        public int InstrumentId { get; set; }
        public int BusinessSeatsNo { get; set; }
        public int RegularSeatsNo { get; set; }
        public int VacantBusinessSeats { get; set; }
        public int VacantRegularSeats { get; set; }
        public decimal TicketCost { get; set; }
        public int NoOfRows { get; set; }
        public int MealPlanId { get; set; }

        public virtual Airline AirLine { get; set; }
        public virtual Flightdaysschedule FlightDaySchedule { get; set; }
        public virtual InstrumentType Instrument { get; set; }
        public virtual Mealplan MealPlan { get; set; }
    }
}
