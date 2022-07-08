using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.ViewModels
{
    public class FlightSearchRequest
    {
        public FlightSearchParamaters OnwardTripRequest { get; set; }
        public FlightSearchParamaters RoundTripRequest { get; set; }
    }
}
