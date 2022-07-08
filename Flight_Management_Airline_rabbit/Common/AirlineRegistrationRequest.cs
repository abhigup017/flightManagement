using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class AirlineRegistrationRequest
    {
        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }
        public string AirlineContact { get; set; }
        public string AirlineAddress { get; set; }
        public string AirlineDescription { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
