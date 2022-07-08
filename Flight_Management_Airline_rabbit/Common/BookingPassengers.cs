using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class BookingPassengers
    {
        public int PassengerId { get; set; }
        public int BookingId { get; set; }
        public string PassengerName { get; set; }
        public int GenderId { get; set; }
        public string GenderType { get; set; }
        public int PassengerAge { get; set; }
        public string SeatNo { get; set; }
        public bool IsBusinessSeat { get; set; }
        public bool IsRegularSeat { get; set; }
    }
}
