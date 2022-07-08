using BookingConsumer.Microservice.Interface;
using BookingConsumer.Microservice.Service;
using Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingConsumer.Microservice
{
    public class BookingRequestConsumer : IConsumer<FlightBookingRequest>
    {
        private readonly ILogger<BookingRequestConsumer> _logger;
        private readonly TicketBookingManagementRepository _ticketBookingManagementRepository;

        public BookingRequestConsumer(ILogger<BookingRequestConsumer> logger)
        {
            _logger = logger;
            _ticketBookingManagementRepository = new TicketBookingManagementRepository();
        }
        /// <summary>
        /// Consumer method to book Flights using FlightBookingRequest Context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
       #region Consumer Method
        public async Task Consume(ConsumeContext<FlightBookingRequest> context)
        {
            try
            {
                string response = _ticketBookingManagementRepository.BookFlightTicket(context.Message);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
            }
        }
        #endregion
    }
}
