using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineConsumer.Microservice.Interface
{
    public interface IAirlineRegistrationManagementRepository
    {
        int RegisterAirline(AirlineRegistrationRequest airlineDetails);
    }
}
