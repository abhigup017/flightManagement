using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.ViewModels
{
    public class FlightSearchResults
    {
        public List<FlightSearchResultParamaters> OnwardTripResults { get; set; }
        public List<FlightSearchResultParamaters> RoundTripResults { get; set; }
    }
}
