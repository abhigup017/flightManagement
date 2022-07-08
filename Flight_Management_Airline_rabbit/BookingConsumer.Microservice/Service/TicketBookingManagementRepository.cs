using BookingConsumer.Microservice.Interface;
using BookingConsumer.Microservice.Models;
using Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingConsumer.Microservice.Service
{
    public class TicketBookingManagementRepository : ITicketBookingManagementRepository
    {
        private readonly FlightManagementContext _flightManagementContext;
        public TicketBookingManagementRepository()
        {
            _flightManagementContext = new FlightManagementContext();
        }
        /// <summary>
        /// Method to create booking in database
        /// </summary>
        /// <param name="flightBookingRequest"></param>
        /// <returns></returns>
        #region Book Flight Tickets
        public string BookFlightTicket(FlightBookingRequest flightBookingRequest)
        {
            string pnrNumber = string.Empty;
           
            using (var transaction = _flightManagementContext.Database.BeginTransaction())
            {
                try
                {
                    List<string> pnrNumbers = new List<string>();
                    pnrNumbers.AddRange(_flightManagementContext.Bookings.Select(x => x.Pnrnumber).ToList());
                    Booking bookingRequest = new Booking
                    {
                        FlightId = flightBookingRequest.FlightId,
                        CustomerName = flightBookingRequest.CustomerName,
                        CustomerEmailId = flightBookingRequest.CustomerEmailId,
                        NoOfSeats = flightBookingRequest.NoOfSeats,
                        Pnrnumber = GeneratePNRNumber(pnrNumbers),
                        MealPlanId = flightBookingRequest.MealPlanId,
                        TravelDate = flightBookingRequest.TravelDate,
                        BookedOn = flightBookingRequest.BookedOn,
                        TotalCost = flightBookingRequest.TotalCost,
                        IsCancelled = false
                    };

                    _flightManagementContext.Bookings.Add(bookingRequest);
                    _flightManagementContext.SaveChanges();

                    if(flightBookingRequest.BookingPassenger != null && flightBookingRequest.BookingPassenger.Count > 0)
                    {
                        foreach(var passenger in flightBookingRequest.BookingPassenger)
                        {
                            Bookingpassenger bookingpassenger = new Bookingpassenger
                            {
                                BookingId = bookingRequest.BookingId,
                                PassengerName = passenger.PassengerName,
                                GenderId = passenger.GenderId,
                                PassengerAge = passenger.PassengerAge,
                                SeatNo = passenger.SeatNo,
                                IsBusinessSeat = passenger.IsBusinessSeat,
                                IsRegularSeat = passenger.IsRegularSeat
                            };

                            _flightManagementContext.Bookingpassengers.Add(bookingpassenger);
                            _flightManagementContext.SaveChanges();
                        }
                        int businessSeatsNo = flightBookingRequest.BookingPassenger.Count(x => x.IsBusinessSeat);
                        int regularSeatsNo = flightBookingRequest.BookingPassenger.Count(x => x.IsRegularSeat);

                        var flightSchedule = _flightManagementContext.Flightschedules.Where(x => x.FlightId == flightBookingRequest.FlightId).FirstOrDefault();
                        flightSchedule.VacantBusinessSeats -= businessSeatsNo;
                        flightSchedule.VacantRegularSeats -= regularSeatsNo;
                        _flightManagementContext.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("An error occurred while Booking.");
                }

                
            }
            return pnrNumber;
        }
        #endregion

        #region Generate PNR Number
        private string GeneratePNRNumber(List<string> pnrNumbers)
        {
            string pnrNumber = string.Empty;
            try
            {
                bool isUniquePnrFound = false;
                Random r = new Random();

                while (isUniquePnrFound == false)
                {
                    pnrNumber = r.Next().ToString();
                    if (!pnrNumbers.Any(x => x == pnrNumber))
                        isUniquePnrFound = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return pnrNumber;
        }
        #endregion
    }
}
