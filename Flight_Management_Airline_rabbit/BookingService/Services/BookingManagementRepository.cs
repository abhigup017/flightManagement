using BookingService.Interfaces;
using BookingService.Models;
using BookingService.ViewModels;
using Common;
using MassTransit;
using MassTransit.KafkaIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Services
{
    public class BookingManagementRepository : IBookingManagementRepository
    {
        private readonly FlightManagementContext _dbContext;
        private readonly IBus _bus;
       

        public BookingManagementRepository(FlightManagementContext dbContext, IBus bus)
        {
            _dbContext = dbContext;
            _bus = bus;
        }

        /// <summary>
        /// Invokes the consumer method to book flight tickets
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="bookingRequest"></param>
        #region Book Flight Tickets
        public async void BookFlightTickets(int flightId, FlightBookingRequest bookingRequest)
        {
            try
            {
                if (bookingRequest != null)
                {
                    bookingRequest.BookedOn = DateTime.Now;
                    bookingRequest.FlightId = flightId;
                    Uri uri = new Uri("rabbitmq://localhost/bookingQueue");
                    var endpoint = await _bus.GetSendEndpoint(uri);
                    await endpoint.Send(bookingRequest);
                }
                else
                {
                    throw new Exception("Invalid Booking Request.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// Get all booked tickets history of a user from user's email Id
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns>List of booked tickets history</returns>
        #region Get Booked Tickets history
        public List<BookedTicketsHistory> GetBookedTicketsHistory(string emailId)
        {
            List<BookedTicketsHistory> response = new List<BookedTicketsHistory>();

            try
            {
                if (!string.IsNullOrEmpty(emailId))
                {
                    response = (from bookings in _dbContext.Bookings
                                join schedule in _dbContext.Flightschedules
                                on bookings.FlightId equals schedule.FlightId
                                join airline in _dbContext.Airlines
                                on schedule.AirLineId equals airline.AirLineId
                                join mealPlan in _dbContext.Mealplans
                                on bookings.MealPlanId equals mealPlan.MealPlanId
                                join scheduleDay in _dbContext.Flightdaysschedules
                                on schedule.FlightDayScheduleId equals scheduleDay.FlightDayScheduleId
                                join sourceLocation in _dbContext.Locations
                                on scheduleDay.SourceLocationId equals sourceLocation.LocationId
                                join destinationLocation in _dbContext.Locations
                                on scheduleDay.DestinationLocationId equals destinationLocation.LocationId
                                where bookings.CustomerEmailId == emailId
                                select new BookedTicketsHistory
                                {
                                    AirlineLogo = airline.AirlineLogo,
                                    AirlineName = airline.AirlineName,
                                    FlightNumber = schedule.FlightNumber,
                                    TotalCost = bookings.TotalCost,
                                    TravellingDate = bookings.TravelDate,
                                    BookingId = bookings.BookingId,
                                    PnrNumber = bookings.Pnrnumber,
                                    IsCancelled = (bool)bookings.IsCancelled,
                                    IsCancellationAllowed = (bool)bookings.IsCancelled ? false : Convert.ToInt32(bookings.TravelDate.Subtract(DateTime.Now).TotalHours) > 24 ? true : false,
                                    CustomerName = bookings.CustomerName,
                                    CustomerEmailId = bookings.CustomerEmailId,
                                    NoOfSeats = bookings.NoOfSeats,
                                    MealPlanId = bookings.MealPlanId,
                                    MealPlanType = mealPlan.MealPlanType,
                                    BookedOn = bookings.BookedOn,
                                    SourceLocation = sourceLocation.LocationName,
                                    DestinationLocation = destinationLocation.LocationName
                                }).OrderByDescending(x => x.TravellingDate).ToList();

                    if(response != null && response.Count > 0)
                    {
                        foreach(var booking in response)
                        {
                            //booking.BookingPassengers = new List<BookingPassengers>();
                            booking.BookingPassengers = (from passenger in _dbContext.Bookingpassengers
                                                         join gender in _dbContext.Gendertypes
                                                         on passenger.GenderId equals gender.GenderId
                                                         where passenger.BookingId == booking.BookingId
                                                         select new BookingPassengers
                                                         {
                                                             PassengerId = passenger.PassengerId,
                                                             BookingId = passenger.BookingId,
                                                             PassengerName = passenger.PassengerName,
                                                             GenderId = passenger.GenderId,
                                                             GenderType = gender.GenderValue,
                                                             PassengerAge = passenger.PassengerAge,
                                                             SeatNo = passenger.SeatNo,
                                                             IsBusinessSeat = passenger.IsBusinessSeat,
                                                             IsRegularSeat = passenger.IsRegularSeat
                                                         }).ToList();
                        }
                    }
                }
                else
                {
                    throw new Exception("Please enter a Email-Id.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
        #endregion
        /// <summary>
        /// Cancel a booked ticket from its PNR Number
        /// </summary>
        /// <param name="pnrNumber"></param>
        /// <returns>Boolean value</returns>
#region Cancel Booking
        public bool CancelBooking(string pnrNumber)
        {
            bool isCancelled = false;

            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if(_dbContext.Bookings.Any(x => x.Pnrnumber == pnrNumber))
                    {
                        var booking = _dbContext.Bookings.Where(x => x.Pnrnumber == pnrNumber).FirstOrDefault();
                        booking.IsCancelled = true;
                        _dbContext.SaveChanges();

                        var bookingPassengers = _dbContext.Bookingpassengers.Where(x => x.BookingId == booking.BookingId).ToList();
                        int businessSeatCount = bookingPassengers.Count(x => x.IsBusinessSeat);
                        int regularSeatCount = bookingPassengers.Count(x => x.IsRegularSeat);
                        var flightSchedule = _dbContext.Flightschedules.Where(x => x.FlightId == booking.FlightId).FirstOrDefault();
                        flightSchedule.VacantBusinessSeats += businessSeatCount;
                        flightSchedule.VacantRegularSeats += regularSeatCount;
                        _dbContext.SaveChanges();
                        transaction.Commit();
                        isCancelled = true;
                    }
                    else
                    {
                        throw new Exception("PNR Number does not exist.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return isCancelled;
        }
        #endregion

        #region Get All Booked Tickets
        /// <summary>
        /// Searches for all booked tickets in the system
        /// </summary>
        /// <param name="bookedTicketsSearchRequest"></param>
        /// <returns>List of all booked tickets</returns>
        public List<BookedTicketsHistory> GetAllBookedTickets(BookedTicketsSearchRequest bookedTicketsSearchRequest)
        {
            List<BookedTicketsHistory> bookedTicketsHistories = new List<BookedTicketsHistory>();
            try
            {
                bookedTicketsHistories = (from bookings in _dbContext.Bookings
                            join schedule in _dbContext.Flightschedules
                            on bookings.FlightId equals schedule.FlightId
                            join airline in _dbContext.Airlines
                            on schedule.AirLineId equals airline.AirLineId
                            join mealPlan in _dbContext.Mealplans
                            on bookings.MealPlanId equals mealPlan.MealPlanId
                            join scheduleDay in _dbContext.Flightdaysschedules
                            on schedule.FlightDayScheduleId equals scheduleDay.FlightDayScheduleId
                            join sourceLocation in _dbContext.Locations
                            on scheduleDay.SourceLocationId equals sourceLocation.LocationId
                            join destinationLocation in _dbContext.Locations
                            on scheduleDay.DestinationLocationId equals destinationLocation.LocationId
                            select new BookedTicketsHistory
                            {
                                AirlineLogo = airline.AirlineLogo,
                                AirlineId = airline.AirLineId,
                                AirlineName = airline.AirlineName,
                                FlightNumber = schedule.FlightNumber,
                                TotalCost = bookings.TotalCost,
                                TravellingDate = bookings.TravelDate,
                                BookingId = bookings.BookingId,
                                PnrNumber = bookings.Pnrnumber,
                                IsCancelled = (bool)bookings.IsCancelled,
                                IsCancellationAllowed = (bool)bookings.IsCancelled ? false : Convert.ToInt32(bookings.TravelDate.Subtract(DateTime.Now).TotalHours) > 24 ? true : false,
                                CustomerName = bookings.CustomerName,
                                CustomerEmailId = bookings.CustomerEmailId,
                                NoOfSeats = bookings.NoOfSeats,
                                MealPlanId = bookings.MealPlanId,
                                MealPlanType = mealPlan.MealPlanType,
                                BookedOn = bookings.BookedOn,
                                SourceLocation = sourceLocation.LocationName,
                                SourceLocationId = sourceLocation.LocationId,
                                DestinationLocation = destinationLocation.LocationName,
                                DestinationLocationId = destinationLocation.LocationId
                            }).OrderByDescending(x => x.TravellingDate).ToList();

                if (bookedTicketsHistories != null && bookedTicketsHistories.Count > 0)
                {
                    if(bookedTicketsSearchRequest.AirlineId > 0)
                    {
                        bookedTicketsHistories = bookedTicketsHistories.Where(x => x.AirlineId == bookedTicketsSearchRequest.AirlineId).ToList();
                    }
                    if(bookedTicketsSearchRequest.SourceId > 0)
                    {
                        bookedTicketsHistories = bookedTicketsHistories.Where(x => x.SourceLocationId == bookedTicketsSearchRequest.SourceId).ToList();
                    }
                    if(bookedTicketsSearchRequest.DestinationId > 0)
                    {
                        bookedTicketsHistories = bookedTicketsHistories.Where(x => x.DestinationLocationId == bookedTicketsSearchRequest.DestinationId).ToList();
                    }
                    if(bookedTicketsSearchRequest.TravelDate != null)
                    {
                        DateTime TravelDate = (DateTime)bookedTicketsSearchRequest.TravelDate;
                        bookedTicketsHistories = bookedTicketsHistories.Where(x => x.TravellingDate.Date == TravelDate.Date).ToList();
                    }
                    
                    foreach (var booking in bookedTicketsHistories)
                    {
                        //booking.BookingPassengers = new List<BookingPassengers>();
                        booking.BookingPassengers = (from passenger in _dbContext.Bookingpassengers
                                                     join gender in _dbContext.Gendertypes
                                                     on passenger.GenderId equals gender.GenderId
                                                     where passenger.BookingId == booking.BookingId
                                                     select new BookingPassengers
                                                     {
                                                         PassengerId = passenger.PassengerId,
                                                         BookingId = passenger.BookingId,
                                                         PassengerName = passenger.PassengerName,
                                                         GenderId = passenger.GenderId,
                                                         GenderType = gender.GenderValue,
                                                         PassengerAge = passenger.PassengerAge,
                                                         SeatNo = passenger.SeatNo,
                                                         IsBusinessSeat = passenger.IsBusinessSeat,
                                                         IsRegularSeat = passenger.IsRegularSeat
                                                     }).ToList();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return bookedTicketsHistories;
        }
        #endregion
    }
}

