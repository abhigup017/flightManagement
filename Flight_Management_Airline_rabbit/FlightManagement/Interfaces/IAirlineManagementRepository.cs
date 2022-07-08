using AirlineService.ViewModels;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Interfaces
{
    public interface IAirlineManagementRepository
    {
        void RegisterAirline(AirlineRegistrationRequest airlineDetails);
        bool AddAirlineInventory(AirlineInventorySchedule airlineInventorySchedule);
        bool BlockAirline(int airlineId);
    }
}
