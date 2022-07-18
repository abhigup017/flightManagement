using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.ViewModels
{
    public class AirlineScheduleDetails
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public int AirLineId { get; set; }
        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }
        public int FlightDayScheduleId { get; set; }
        public int InstrumentId { get; set; }
        public string InstrumentType { get; set; }
        public int BusinessSeatsNo { get; set; }
        public int RegularSeatsNo { get; set; }
        public decimal TicketCost { get; set; }
        public int NoOfRows { get; set; }
        public int MealPlanId { get; set; }
        public string MealPlanType { get; set; }
        public int SourceLocationId { get; set; }
        public string SourceLocation { get; set; }
        public int DestinationLocationId { get; set; }
        public string DestinationLocation { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
