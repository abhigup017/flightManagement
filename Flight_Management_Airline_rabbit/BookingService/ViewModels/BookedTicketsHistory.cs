using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.ViewModels
{
    public class BookedTicketsHistory
    {
        public string AirlineLogo { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime TravellingDate { get; set; }
        public int BookingId { get; set; }
        public string PnrNumber { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsCancellationAllowed { get; set; }
    }
}
