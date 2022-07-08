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
                    response = (from airline in _dbContext.Airlines
                                join bookings in _dbContext.Bookings
                                on airline.AirLineId equals bookings.FlightId
                                where bookings.CustomerEmailId == emailId
                                select new BookedTicketsHistory
                                {
                                    AirlineLogo = airline.AirlineLogo,
                                    TotalCost = bookings.TotalCost,
                                    TravellingDate = bookings.TravelDate,
                                    BookingId = bookings.BookingId,
                                    PnrNumber = bookings.Pnrnumber,
                                    IsCancelled = (bool)bookings.IsCancelled,
                                    IsCancellationAllowed = (bool)bookings.IsCancelled ? false : Convert.ToInt32(bookings.TravelDate.Subtract(DateTime.Now).TotalHours) > 24 ? true : false
                                }).OrderByDescending(x => x.TravellingDate).ToList();
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
    }
}

