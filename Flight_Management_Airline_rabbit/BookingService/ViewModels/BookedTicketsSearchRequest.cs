using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.ViewModels
{
    public class BookedTicketsSearchRequest
    {
        public int AirlineId { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public DateTime? TravelDate { get; set; }
    }
}
