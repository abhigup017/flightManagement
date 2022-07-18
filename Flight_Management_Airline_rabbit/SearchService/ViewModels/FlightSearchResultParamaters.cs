using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.ViewModels
{
    public class FlightSearchResultParamaters
    {
        public int FlightId { get; set; }
        public int FlightDayScheduleId { get; set; }
        public DateTime FlightDateTime { get; set; }
        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }
        public decimal Cost { get; set; }
        public int MealPlanId { get; set; }
        public int VacantBusinessSeats { get; set; }
        public int VacantRegularSeats { get; set; }
        public string FlightNumber { get; set; }

    }
}
