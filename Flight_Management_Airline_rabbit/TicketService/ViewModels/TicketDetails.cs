using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketService.ViewModels
{
    public class TicketDetails
    {
        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmailId { get; set; }
        public int NoOfSeats { get; set; }
        public int MealPlanId { get; set; }
        public string MealPlanType { get; set; }
        public string Pnrnumber { get; set; }
        public DateTime TravelDate { get; set; }
        public DateTime BookedOn { get; set; }
        public decimal TotalCost { get; set; }
        public bool? IsCancelled { get; set; }
        public List<BookingPassengers> BookingPassenger { get; set; }
    }
}
