using System;
using System.Collections.Generic;

#nullable disable

namespace AirlineService.Models
{
    public partial class Bookingpassenger
    {
        public int PassengerId { get; set; }
        public int BookingId { get; set; }
        public string PassengerName { get; set; }
        public int GenderId { get; set; }
        public int PassengerAge { get; set; }
        public string SeatNo { get; set; }
        public bool IsBusinessSeat { get; set; }
        public bool IsRegularSeat { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
