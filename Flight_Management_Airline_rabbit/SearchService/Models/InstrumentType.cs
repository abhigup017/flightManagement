using System;
using System.Collections.Generic;

#nullable disable

namespace SearchService.Models
{
    public partial class InstrumentType
    {
        public InstrumentType()
        {
            Flightschedules = new HashSet<Flightschedule>();
        }

        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }

        public virtual ICollection<Flightschedule> Flightschedules { get; set; }
    }
}
