using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.ViewModels
{
    public class BookingPassengers
    {
        public int BookingId { get; set; }
        public string PassengerName { get; set; }
        public int GenderId { get; set; }
        public int PassengerAge { get; set; }
    }
}
