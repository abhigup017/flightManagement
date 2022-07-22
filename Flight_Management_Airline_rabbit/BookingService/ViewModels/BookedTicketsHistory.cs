using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.ViewModels
{
    public class BookedTicketsHistory
    {
        public string AirlineLogo { get; set; }
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public string FlightNumber { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime TravellingDate { get; set; }
        public int BookingId { get; set; }
        public string PnrNumber { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsCancellationAllowed { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmailId { get; set; }
        public int NoOfSeats { get; set; }
        public int MealPlanId { get; set; }
        public string MealPlanType { get; set; }
        public DateTime BookedOn { get; set; }
        public string SourceLocation { get; set; }
        public int SourceLocationId { get; set; }
        public string DestinationLocation { get; set; }
        public int DestinationLocationId { get; set; }
        public List<BookingPassengers> BookingPassengers { get; set; }
    }
}
