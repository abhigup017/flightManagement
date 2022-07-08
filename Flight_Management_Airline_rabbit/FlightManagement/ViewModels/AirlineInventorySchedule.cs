using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.ViewModels
{
    public class AirlineInventorySchedule
    {
        public string FlightNumber { get; set; }
        public int AirLineId { get; set; }
        public int FlightDayScheduleId { get; set; }
        public int InstrumentId { get; set; }
        public int BusinessSeatsNo { get; set; }
        public int RegularSeatsNo { get; set; }
        public decimal TicketCost { get; set; }
        public int NoOfRows { get; set; }
        public int MealPlanId { get; set; }
        public int SourceLocationId { get; set; }
        public int DestinationLocationId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int DurationInMinutes { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}
