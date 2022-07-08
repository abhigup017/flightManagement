using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.ViewModels
{
    public class FlightSearchParamaters
    {
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public DateTime TravelDateTime { get; set; }
        public bool IsTimeBasedSearch { get; set; }

    }
}
