using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.ViewModels
{
    public class SearchScheduleRequest
    {
        public int AirlineId { get; set; }
        public string FlightNumber { get; set; }
        public int InstrumentUsed { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public int SourceLocationId { get; set; }
        public int DestinationLocationId { get; set; }
    }
}
