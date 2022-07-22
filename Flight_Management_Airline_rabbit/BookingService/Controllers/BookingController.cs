using BookingService.Interfaces;
using BookingService.ViewModels;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/flight/booking")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingManagementRepository _bookingManagementRepository;
        private readonly ILogger logger;
        public BookingController(IBookingManagementRepository bookingManagementRepository, ILogger<BookingController> _logger)
        {
            _bookingManagementRepository = bookingManagementRepository;
            logger = _logger;
        }

        #region Book Flight Tickets
        [HttpPost, Route("{flightId}")]
        public IActionResult BookFlightTickets(int flightId, FlightBookingRequest bookingRequest)
        {
            try
            {
                _bookingManagementRepository.BookFlightTickets(flightId, bookingRequest);
                return Ok();
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Get Booked Tickets from Email
        [HttpGet, Route("history/{emailId}")]
        public IActionResult GetBookedTicketsHistory(string emailId)
        {
            try
            {
                var response = _bookingManagementRepository.GetBookedTicketsHistory(emailId);
                return Ok(response);
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Cancel Booking
        [HttpDelete, Route("cancel/{pnrNumber}")]
        public IActionResult CancelBooking(string pnrNumber)
        {
            try
            {
                var response = _bookingManagementRepository.CancelBooking(pnrNumber);
                return Ok(new { isCancelled = response });
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Get all Booked Tickets
        [HttpPost, Route("history/all")]
        public IActionResult GetAllBookedTickets(BookedTicketsSearchRequest bookedTicketsSearchRequest)
        {
            try
            {
                var bookedTicketsHistory = _bookingManagementRepository.GetAllBookedTickets(bookedTicketsSearchRequest);
                return Ok(bookedTicketsHistory);
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
    }
}
