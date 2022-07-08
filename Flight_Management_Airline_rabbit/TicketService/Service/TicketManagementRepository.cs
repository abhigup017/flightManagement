﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Interface;
using TicketService.Models;
using TicketService.ViewModels;

namespace TicketService.Service
{
    public class TicketManagementRepository : ITicketManagementRepository
    {
        private readonly FlightManagementContext _flightManagementContext;

        public TicketManagementRepository(FlightManagementContext flightManagementContext)
        {
            _flightManagementContext = flightManagementContext;
        }
        /// <summary>
        /// Get ticket details from PNR number
        /// </summary>
        /// <param name="PNRNumber"></param>
        /// <returns>Ticket details</returns>
       #region Get Ticket from PNR
        public TicketDetails GetTicketDetailsFromPNR(string PNRNumber)
        {
            TicketDetails response = new TicketDetails();
            response.BookingPassenger = new List<BookingPassengers>();

            try
            {
                if (string.IsNullOrEmpty(PNRNumber))
                    throw new Exception("Please enter a PNR Number!");
                //First check if pnr number is valid
                if(_flightManagementContext.Bookings.Any(x => x.Pnrnumber == PNRNumber))
                {
                    response = (from booking in _flightManagementContext.Bookings
                                join mealPlan in _flightManagementContext.Mealplans
                                on booking.MealPlanId equals mealPlan.MealPlanId
                                where booking.Pnrnumber == PNRNumber
                                select new TicketDetails
                                {
                                    BookingId = booking.BookingId,
                                    FlightId = booking.FlightId,
                                    CustomerName = booking.CustomerName,
                                    CustomerEmailId = booking.CustomerEmailId,
                                    NoOfSeats = booking.NoOfSeats,
                                    MealPlanId = booking.MealPlanId,
                                    MealPlanType = mealPlan.MealPlanType,
                                    Pnrnumber = booking.Pnrnumber,
                                    TravelDate = booking.TravelDate,
                                    BookedOn = booking.BookedOn,
                                    TotalCost = booking.TotalCost,
                                    IsCancelled = booking.IsCancelled
                                }).FirstOrDefault();

                    response.BookingPassenger = (from bookingPassengers in _flightManagementContext.Bookingpassengers
                                                 join genderTypes in _flightManagementContext.Gendertypes
                                                 on bookingPassengers.GenderId equals genderTypes.GenderId
                                                 where bookingPassengers.BookingId == response.BookingId
                                                 select new BookingPassengers
                                                 {
                                                     PassengerId = bookingPassengers.PassengerId,
                                                     PassengerName = bookingPassengers.PassengerName,
                                                     GenderId = bookingPassengers.GenderId,
                                                     GenderType = genderTypes.GenderValue,
                                                     PassengerAge = bookingPassengers.PassengerAge,
                                                     SeatNo = bookingPassengers.SeatNo
                                                 }).ToList();
                                
                }
                else
                {
                    throw new Exception("PNR Number entered is Invalid!");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return response;
        }
        #endregion
    }
}
