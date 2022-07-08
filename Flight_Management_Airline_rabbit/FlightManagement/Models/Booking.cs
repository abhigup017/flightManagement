using System;
using System.Collections.Generic;

#nullable disable

namespace AirlineService.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Bookingpassengers = new HashSet<Bookingpassenger>();
        }

        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmailId { get; set; }
        public int NoOfSeats { get; set; }
        public int MealPlanId { get; set; }
        public string Pnrnumber { get; set; }
        public DateTime TravelDate { get; set; }
        public DateTime BookedOn { get; set; }
        public decimal TotalCost { get; set; }
        public bool? IsCancelled { get; set; }

        public virtual Mealplan MealPlan { get; set; }
        public virtual ICollection<Bookingpassenger> Bookingpassengers { get; set; }
    }
}
