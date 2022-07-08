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
    }
}
