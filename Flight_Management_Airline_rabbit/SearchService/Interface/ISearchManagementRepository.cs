using SearchService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.Interface
{
    public interface ISearchManagementRepository
    {
        FlightSearchResults SearchFlightsForBooking(FlightSearchRequest flightSearchRequest);
    }
}
