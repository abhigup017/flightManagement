using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.ViewModels
{
    public class AirlineDetails
    {
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }
        public string AirlineContact { get; set; }
        public string AirlineAddress { get; set; }
        public string AirlineDescription { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
