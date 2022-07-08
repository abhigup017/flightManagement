using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.ViewModels;

namespace TicketService.Interface
{
    public interface ITicketManagementRepository
    {
        TicketDetails GetTicketDetailsFromPNR(string PNRNumber);
    }
}
