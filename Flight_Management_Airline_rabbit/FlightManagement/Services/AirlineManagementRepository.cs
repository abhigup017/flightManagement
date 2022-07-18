using AirlineService.Interfaces;
using AirlineService.Models;
using AirlineService.ViewModels;
using Common;
using MassTransit;
using MassTransit.KafkaIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Services
{
    public class AirlineManagementRepository : IAirlineManagementRepository
    {
        private readonly FlightManagementContext _flightManagementContext;
        private readonly IBus _bus;

        public AirlineManagementRepository(FlightManagementContext flightManagementContext, IBus bus)
        {
            _flightManagementContext = flightManagementContext;
            _bus = bus;
        }
        /// <summary>
        /// This method Registers a new airline in the system by calling the consumer
        /// </summary>
        /// <param name="airlineDetails"></param>
        /// <returns>Void</returns>
        #region Register Airline
        public async void RegisterAirline(AirlineRegistrationRequest airlineDetails)
        {
            int insertedId = 0;

            try
            {
                if (airlineDetails != null)
                {
                    Uri uri = new Uri("rabbitmq://localhost/airlineQueue");
                    var endpoint = await _bus.GetSendEndpoint(uri);
                    await endpoint.Send(airlineDetails);
                }
                else
                {
                    throw new Exception("Invalid Request");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding airline");
            }
        }
        #endregion
        /// <summary>
        /// This method adds a Inventory schedule for a airline
        /// </summary>
        /// <param name="airlineInventorySchedule"></param>
        /// <returns>boolean value</returns>
#region Add Airline Inventory
        public bool AddAirlineInventory(AirlineInventorySchedule airlineInventorySchedule)
        {
            bool isInserted = false;

            try
            {
                //first find all the days between start date and end date based on days selected
                List<DateTime> days = new List<DateTime>();

                if (airlineInventorySchedule.Monday)
                    GetDays("Monday", airlineInventorySchedule.StartDateTime, airlineInventorySchedule.EndDateTime, ref days);
                if (airlineInventorySchedule.Tuesday)
                    GetDays("Tuesday", airlineInventorySchedule.StartDateTime, airlineInventorySchedule.EndDateTime, ref days);
                if (airlineInventorySchedule.Wednesday)
                    GetDays("Wednesday", airlineInventorySchedule.StartDateTime, airlineInventorySchedule.EndDateTime, ref days);
                if (airlineInventorySchedule.Thursday)
                    GetDays("Thursday", airlineInventorySchedule.StartDateTime, airlineInventorySchedule.EndDateTime, ref days);
                if (airlineInventorySchedule.Friday)
                    GetDays("Friday", airlineInventorySchedule.StartDateTime, airlineInventorySchedule.EndDateTime, ref days);
                if (airlineInventorySchedule.Saturday)
                    GetDays("Saturday", airlineInventorySchedule.StartDateTime, airlineInventorySchedule.EndDateTime, ref days);
                if (airlineInventorySchedule.Sunday)
                    GetDays("Sunday", airlineInventorySchedule.StartDateTime, airlineInventorySchedule.EndDateTime, ref days);

                using(var transaction = _flightManagementContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (days != null && days.Count > 0)
                        {
                            foreach (var day in days)
                            {
                                Flightdaysschedule flightdaysschedule = new Flightdaysschedule
                                {
                                    SourceLocationId = airlineInventorySchedule.SourceLocationId,
                                    DestinationLocationId = airlineInventorySchedule.DestinationLocationId,
                                    StartDateTime = day,
                                    EndDateTime = day.AddMinutes(airlineInventorySchedule.DurationInMinutes)
                                };

                                _flightManagementContext.Flightdaysschedules.Add(flightdaysschedule);
                                _flightManagementContext.SaveChanges();
                                int flightDayScheduleId = flightdaysschedule.FlightDayScheduleId;

                                Flightschedule flightschedule = new Flightschedule
                                {
                                    FlightNumber = airlineInventorySchedule.FlightNumber,
                                    AirLineId = airlineInventorySchedule.AirLineId,
                                    FlightDayScheduleId = flightDayScheduleId,
                                    InstrumentId = airlineInventorySchedule.InstrumentId,
                                    BusinessSeatsNo = airlineInventorySchedule.BusinessSeatsNo,
                                    RegularSeatsNo = airlineInventorySchedule.RegularSeatsNo,
                                    TicketCost = airlineInventorySchedule.TicketCost,
                                    NoOfRows = airlineInventorySchedule.NoOfRows,
                                    MealPlanId = airlineInventorySchedule.MealPlanId,
                                    VacantBusinessSeats = airlineInventorySchedule.BusinessSeatsNo,
                                    VacantRegularSeats = airlineInventorySchedule.RegularSeatsNo
                                };

                                _flightManagementContext.Flightschedules.Add(flightschedule);
                                _flightManagementContext.SaveChanges();
                            }
                            transaction.Commit();
                            isInserted = true;
                        }
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return isInserted;
        }
        #endregion

        #region Generate Days
        private void GetDays(string day, DateTime startDate, DateTime endDate,ref List<DateTime> days)
        {
            try
            {
                DateTime date = startDate;

                switch (day)
                {
                    case "Monday":
                        while (date.DayOfWeek != DayOfWeek.Monday)
                        {
                            date = date.AddDays(1);
                        }
                        break;
                    case "Tuesday":
                        while (date.DayOfWeek != DayOfWeek.Tuesday)
                        {
                            date = date.AddDays(1);
                        }
                        break;
                    case "Wednesday":
                        while (date.DayOfWeek != DayOfWeek.Wednesday)
                        {
                            date = date.AddDays(1);
                        }
                        break;
                    case "Thursday":
                        while (date.DayOfWeek != DayOfWeek.Thursday)
                        {
                            date = date.AddDays(1);
                        }
                        break;
                    case "Friday":
                        while (date.DayOfWeek != DayOfWeek.Friday)
                        {
                            date = date.AddDays(1);
                        }
                        break;
                    case "Saturday":
                        while (date.DayOfWeek != DayOfWeek.Saturday)
                        {
                            date = date.AddDays(1);
                        }
                        break;
                    case "Sunday":
                        while (date.DayOfWeek != DayOfWeek.Sunday)
                        {
                            date = date.AddDays(1);
                        }
                        break;

                }
                days.Add(date);
                bool canAddDays = true;
                while (canAddDays)
                {
                    date = date.AddDays(7);
                    if (DateTime.Compare(date, endDate) <= 0)
                        days.Add(date);
                    else
                        canAddDays = false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Block Airline
        public bool BlockAirline(int airlineId)
        {
            bool isBlocked = false;
            using(var transaction = _flightManagementContext.Database.BeginTransaction())
            {
                try
                {
                    var airlineDetails = _flightManagementContext.Airlines.Where(x => x.AirLineId == airlineId).FirstOrDefault();
                    if (airlineDetails != null && airlineDetails.AirLineId <= 0)
                        throw new Exception("Airline Id does not exists!");

                    airlineDetails.IsBlocked = true;
                    _flightManagementContext.SaveChanges();
                    transaction.Commit();
                    isBlocked = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return isBlocked;
        }
        #endregion

        #region GetAllAirlines
        /// <summary>
        /// Gets all the registered airline
        /// </summary>
        /// <returns>List of registered airlines</returns>
        public List<AirlineDetails> GetAllAirlines()
        {
            List<AirlineDetails> airlineDetails = new List<AirlineDetails>();
            try
            {
                airlineDetails = (from airline in _flightManagementContext.Airlines
                                  select new AirlineDetails
                                  {
                                      AirlineId = airline.AirLineId,
                                      AirlineName = airline.AirlineName,
                                      AirlineAddress = airline.AirlineAddress,
                                      AirlineContact = airline.AirlineContact,
                                      AirlineDescription = airline.AirlineDescription,
                                      AirlineLogo = airline.AirlineLogo,
                                      IsBlocked = airline.IsBlocked
                                  }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return airlineDetails;
        }
        #endregion

        #region Unblock Airline
        /// <summary>
        /// Unblocks a airline
        /// </summary>
        /// <param name="airlineId"></param>
        /// <returns>A boolen flag</returns>
        public bool UnBlockAirline(int airlineId)
        {
            bool isUnblocked = false;
            using (var transaction = _flightManagementContext.Database.BeginTransaction())
            {
                try
                {
                    var airline = _flightManagementContext.Airlines.Where(x => x.AirLineId == airlineId).FirstOrDefault();
                    airline.IsBlocked = false;
                    _flightManagementContext.SaveChanges();
                    transaction.Commit();
                    isUnblocked = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return isUnblocked;
        }
        #endregion

        #region Delete Airline
        /// <summary>
        /// Deletes an Airline from the airline id
        /// </summary>
        /// <param name="airlineId"></param>
        /// <returns>A boolean flag</returns>
        public bool DeleteAirline(int airlineId)
        {
            bool isDeleted = false;

            using (var transaction = _flightManagementContext.Database.BeginTransaction())
            {
                try
                {
                    var airline = _flightManagementContext.Airlines.Where(x => x.AirLineId == airlineId).FirstOrDefault();
                    if(airline != null && airline.AirLineId > 0)
                    {
                        _flightManagementContext.Airlines.Remove(airline);
                        _flightManagementContext.SaveChanges();
                        isDeleted = true;
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return isDeleted;
        }
        #endregion

        #region Search Airline schedules
        /// <summary>
        /// Searches for airline schedules from the search criteria
        /// </summary>
        /// <param name="searchScheduleRequest"></param>
        /// <returns>List of airline schedules</returns>
        public List<AirlineScheduleDetails> SearchSchedules(SearchScheduleRequest searchScheduleRequest)
        {
            List<AirlineScheduleDetails> airlineScheduleDetails = new List<AirlineScheduleDetails>();

            try
            {
                airlineScheduleDetails = (from schedule in _flightManagementContext.Flightschedules
                                          join daySchedule in _flightManagementContext.Flightdaysschedules
                                          on schedule.FlightDayScheduleId equals daySchedule.FlightDayScheduleId
                                          join airline in _flightManagementContext.Airlines
                                          on schedule.AirLineId equals airline.AirLineId
                                          join instrument in _flightManagementContext.InstrumentTypes
                                          on schedule.InstrumentId equals instrument.InstrumentId
                                          join mealPlan in _flightManagementContext.Mealplans
                                          on schedule.MealPlanId equals mealPlan.MealPlanId
                                          join sourceLocation in _flightManagementContext.Locations
                                          on daySchedule.SourceLocationId equals sourceLocation.LocationId
                                          join destinationLocation in _flightManagementContext.Locations
                                          on daySchedule.DestinationLocationId equals destinationLocation.LocationId
                                          select new AirlineScheduleDetails
                                          {
                                              FlightId = schedule.FlightId,
                                              FlightNumber = schedule.FlightNumber,
                                              AirLineId = schedule.AirLineId,
                                              AirlineName = airline.AirlineName,
                                              AirlineLogo = airline.AirlineLogo,
                                              FlightDayScheduleId = schedule.FlightDayScheduleId,
                                              InstrumentId = schedule.InstrumentId,
                                              InstrumentType = instrument.InstrumentName,
                                              BusinessSeatsNo = schedule.BusinessSeatsNo,
                                              RegularSeatsNo = schedule.RegularSeatsNo,
                                              TicketCost = schedule.TicketCost,
                                              NoOfRows = schedule.NoOfRows,
                                              MealPlanId = schedule.MealPlanId,
                                              MealPlanType = mealPlan.MealPlanType,
                                              SourceLocationId = daySchedule.SourceLocationId,
                                              SourceLocation = sourceLocation.LocationName,
                                              DestinationLocationId = daySchedule.DestinationLocationId,
                                              DestinationLocation = destinationLocation.LocationName,
                                              StartDateTime = daySchedule.StartDateTime,
                                              EndDateTime = daySchedule.EndDateTime,
                                              DurationInMinutes = 0
                                          }).ToList();

                if(airlineScheduleDetails != null && airlineScheduleDetails.Count > 0)
                {
                    if(searchScheduleRequest.AirlineId > 0)
                    {
                        airlineScheduleDetails = airlineScheduleDetails.Where(x => x.AirLineId == searchScheduleRequest.AirlineId).ToList();
                    }
                    if(!string.IsNullOrEmpty(searchScheduleRequest.FlightNumber))
                    {
                        airlineScheduleDetails = airlineScheduleDetails.Where(x => x.FlightNumber.ToLower().Contains(searchScheduleRequest.FlightNumber.ToLower())).ToList();
                    }
                    if(searchScheduleRequest.InstrumentUsed > 0)
                    {
                        airlineScheduleDetails = airlineScheduleDetails.Where(x => x.InstrumentId == searchScheduleRequest.InstrumentUsed).ToList();
                    }
                    if(searchScheduleRequest.SourceLocationId > 0)
                    {
                        airlineScheduleDetails = airlineScheduleDetails.Where(x => x.SourceLocationId == searchScheduleRequest.SourceLocationId).ToList();
                    }
                    if(searchScheduleRequest.DestinationLocationId > 0)
                    {
                        airlineScheduleDetails = airlineScheduleDetails.Where(x => x.DestinationLocationId == searchScheduleRequest.DestinationLocationId).ToList();
                    }
                    if(searchScheduleRequest.ScheduleDate != null)
                    {
                        var startDate = Convert.ToDateTime(searchScheduleRequest.ScheduleDate);
                        airlineScheduleDetails = airlineScheduleDetails.Where(x => x.StartDateTime.Date == startDate.Date).ToList();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return airlineScheduleDetails;
        }
        #endregion
    }
}
