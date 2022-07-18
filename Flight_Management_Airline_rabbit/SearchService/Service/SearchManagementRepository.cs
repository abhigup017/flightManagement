using SearchService.Interface;
using SearchService.Models;
using SearchService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.Service
{
    public class SearchManagementRepository : ISearchManagementRepository
    {
        private readonly FlightManagementContext _flightManagementContext;

        public SearchManagementRepository(FlightManagementContext flightManagementContext)
        {
            _flightManagementContext = flightManagementContext;
        }
        /// <summary>
        /// Search for flights for onward and return journey both for flight booking
        /// </summary>
        /// <param name="flightSearchRequest"></param>
        /// <returns>List of flight search results</returns>
       #region Search for Flights
        public FlightSearchResults SearchFlightsForBooking(FlightSearchRequest flightSearchRequest)
        {
            FlightSearchResults flightSearchResults = new FlightSearchResults();
            flightSearchResults.OnwardTripResults = new List<FlightSearchResultParamaters>();
            flightSearchResults.RoundTripResults = new List<FlightSearchResultParamaters>();

            try
            {
                //First Get results of Onward Trip
                flightSearchResults.OnwardTripResults = (from airline in _flightManagementContext.Airlines
                                                  join flightSchedule in _flightManagementContext.Flightschedules
                                                  on airline.AirLineId equals flightSchedule.AirLineId
                                                  join flightDaySchedule in _flightManagementContext.Flightdaysschedules
                                                  on flightSchedule.FlightDayScheduleId equals flightDaySchedule.FlightDayScheduleId
                                                  where flightDaySchedule.SourceLocationId == flightSearchRequest.OnwardTripRequest.SourceId
                                                  && flightDaySchedule.DestinationLocationId == flightSearchRequest.OnwardTripRequest.DestinationId
                                                  && flightDaySchedule.StartDateTime.Date == flightSearchRequest.OnwardTripRequest.TravelDateTime.Date
                                                  && airline.IsBlocked == false
                                                  select new FlightSearchResultParamaters
                                                  {
                                                      FlightId = flightSchedule.FlightId,
                                                      FlightDayScheduleId = flightDaySchedule.FlightDayScheduleId,
                                                      FlightDateTime = flightDaySchedule.StartDateTime,
                                                      FlightNumber = flightSchedule.FlightNumber,
                                                      AirlineName = airline.AirlineName,
                                                      AirlineLogo = airline.AirlineLogo,
                                                      Cost = flightSchedule.TicketCost,
                                                      MealPlanId = flightSchedule.MealPlanId,
                                                      VacantBusinessSeats = flightSchedule.VacantBusinessSeats,
                                                      VacantRegularSeats = flightSchedule.VacantRegularSeats
                                                  }).ToList();

                if(flightSearchRequest.OnwardTripRequest.IsTimeBasedSearch && flightSearchResults.OnwardTripResults != null && flightSearchResults.OnwardTripResults.Count > 0)
                {
                    flightSearchResults.OnwardTripResults = flightSearchResults.OnwardTripResults.Where(x => x.FlightDateTime.TimeOfDay <= flightSearchRequest.OnwardTripRequest.TravelDateTime.TimeOfDay).ToList();
                }

                if(flightSearchRequest.RoundTripRequest != null && flightSearchRequest.RoundTripRequest.SourceId > 0)
                {
                    //Get results of Onward Trip
                    flightSearchResults.RoundTripResults = (from airline in _flightManagementContext.Airlines
                                                      join flightSchedule in _flightManagementContext.Flightschedules
                                                      on airline.AirLineId equals flightSchedule.AirLineId
                                                      join flightDaySchedule in _flightManagementContext.Flightdaysschedules
                                                      on flightSchedule.FlightDayScheduleId equals flightDaySchedule.FlightDayScheduleId
                                                      where flightDaySchedule.SourceLocationId == flightSearchRequest.RoundTripRequest.SourceId
                                                      && flightDaySchedule.DestinationLocationId == flightSearchRequest.RoundTripRequest.DestinationId
                                                      && flightDaySchedule.StartDateTime.Date == flightSearchRequest.RoundTripRequest.TravelDateTime.Date
                                                      && airline.IsBlocked == false
                                                      select new FlightSearchResultParamaters
                                                      {
                                                          FlightId = flightSchedule.FlightId,
                                                          FlightDayScheduleId = flightDaySchedule.FlightDayScheduleId,
                                                          FlightDateTime = flightDaySchedule.StartDateTime,
                                                          FlightNumber = flightSchedule.FlightNumber,
                                                          AirlineName = airline.AirlineName,
                                                          AirlineLogo = airline.AirlineLogo,
                                                          Cost = flightSchedule.TicketCost,
                                                          MealPlanId = flightSchedule.MealPlanId,
                                                          VacantBusinessSeats = flightSchedule.VacantBusinessSeats,
                                                          VacantRegularSeats = flightSchedule.VacantRegularSeats
                                                      }).ToList();

                    if (flightSearchRequest.RoundTripRequest.IsTimeBasedSearch && flightSearchResults.RoundTripResults != null && flightSearchResults.RoundTripResults.Count > 0)
                    {
                        flightSearchResults.RoundTripResults = flightSearchResults.RoundTripResults.Where(x => x.FlightDateTime.TimeOfDay <= flightSearchRequest.RoundTripRequest.TravelDateTime.TimeOfDay).ToList();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return flightSearchResults;
        }
        #endregion
    }
}
