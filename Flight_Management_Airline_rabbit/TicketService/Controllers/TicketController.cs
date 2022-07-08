using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Interface;

namespace TicketService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/flight/ticket")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketManagementRepository _ticketManagementRepository;
        private readonly ILogger _logger;
        public TicketController(ITicketManagementRepository ticketManagementRepository, ILogger<TicketController> logger)
        {
            _ticketManagementRepository = ticketManagementRepository;
            _logger = logger;
        }

        #region Get Ticket from PNR
        [HttpGet, Route("{pnr}")]
        public IActionResult GetTicketDetailsFromPNR(string pnr)
        {
            try
            {
                var ticketDetails = _ticketManagementRepository.GetTicketDetailsFromPNR(pnr);
                return Ok(ticketDetails);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
    }
}
