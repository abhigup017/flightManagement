using AirlineConsumer.Microservice.Interface;
using AirlineConsumer.Microservice.Models;
using Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineConsumer.Microservice.Service
{
    public class AirlineRegistrationManagementRepository : IAirlineRegistrationManagementRepository
    {
        private readonly FlightManagementContext _flightManagementContext;

        public AirlineRegistrationManagementRepository()
        {
            _flightManagementContext = new FlightManagementContext();
        }
        /// <summary>
        /// Method to Register Airline in Database
        /// </summary>
        /// <param name="airlineDetails"></param>
        /// <returns>Int value</returns>
       #region Register Airline
        public int RegisterAirline(AirlineRegistrationRequest airlineDetails)
        {
            int insertedId = 0;
            try
            {
                Airline airline = new Airline
                {
                    AirlineName = airlineDetails.AirlineName,
                    AirlineLogo = airlineDetails.AirlineLogo,
                    AirlineAddress = airlineDetails.AirlineAddress,
                    AirlineContact = airlineDetails.AirlineContact,
                    AirlineDescription = airlineDetails.AirlineDescription,
                    IsBlocked = false
                };

                _flightManagementContext.Airlines.Add(airline);
                _flightManagementContext.SaveChanges();
                insertedId = airline.AirLineId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding airline");
            }

            return insertedId;
        }
        #endregion
    }
}
