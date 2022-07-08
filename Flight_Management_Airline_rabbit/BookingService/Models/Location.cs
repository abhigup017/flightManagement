using System;
using System.Collections.Generic;

#nullable disable

namespace BookingService.Models
{
    public partial class Location
    {
        public Location()
        {
            FlightdaysscheduleDestinationLocations = new HashSet<Flightdaysschedule>();
            FlightdaysscheduleSourceLocations = new HashSet<Flightdaysschedule>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<Flightdaysschedule> FlightdaysscheduleDestinationLocations { get; set; }
        public virtual ICollection<Flightdaysschedule> FlightdaysscheduleSourceLocations { get; set; }
    }
}
