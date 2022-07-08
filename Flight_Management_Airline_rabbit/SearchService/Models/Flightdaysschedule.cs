using System;
using System.Collections.Generic;

#nullable disable

namespace SearchService.Models
{
    public partial class Flightdaysschedule
    {
        public Flightdaysschedule()
        {
            Flightschedules = new HashSet<Flightschedule>();
        }

        public int FlightDayScheduleId { get; set; }
        public int SourceLocationId { get; set; }
        public int DestinationLocationId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public virtual Location DestinationLocation { get; set; }
        public virtual Location SourceLocation { get; set; }
        public virtual ICollection<Flightschedule> Flightschedules { get; set; }
    }
}
