using System;
using System.Collections.Generic;

#nullable disable

namespace DiscountService.Models
{
    public partial class Airline
    {
        public Airline()
        {
            Flightschedules = new HashSet<Flightschedule>();
        }

        public int AirLineId { get; set; }
        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }
        public string AirlineContact { get; set; }
        public string AirlineAddress { get; set; }
        public string AirlineDescription { get; set; }
        public bool? IsBlocked { get; set; }

        public virtual ICollection<Flightschedule> Flightschedules { get; set; }
    }
}
