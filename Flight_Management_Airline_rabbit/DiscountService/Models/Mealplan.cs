using System;
using System.Collections.Generic;

#nullable disable

namespace DiscountService.Models
{
    public partial class Mealplan
    {
        public Mealplan()
        {
            Bookings = new HashSet<Booking>();
            Flightschedules = new HashSet<Flightschedule>();
        }

        public int MealPlanId { get; set; }
        public string MealPlanType { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Flightschedule> Flightschedules { get; set; }
    }
}
