using BookingService.ViewModels;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Interfaces
{
    public interface IBookingManagementRepository
    {
        void BookFlightTickets(int flightId, FlightBookingRequest bookingRequest);
        List<BookedTicketsHistory> GetBookedTicketsHistory(string emailId);
        bool CancelBooking(string pnrNumber);
        List<BookedTicketsHistory> GetAllBookedTickets(BookedTicketsSearchRequest bookedTicketsSearchRequest);
    }
}
