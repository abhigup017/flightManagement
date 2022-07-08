using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchService.Interface;
using SearchService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/flight/search")]
    [ApiController]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly ISearchManagementRepository _searchManagementRepository;
        private readonly ILogger _logger;
        public SearchController(ISearchManagementRepository searchManagementRepository, ILogger<SearchController> logger)
        {
            _searchManagementRepository = searchManagementRepository;
            _logger = logger;
        }

        #region Search Flights
        [HttpPost]
        public IActionResult SearchFlightsForBooking(FlightSearchRequest flightSearchRequest)
        {
            try
            {
                var response = _searchManagementRepository.SearchFlightsForBooking(flightSearchRequest);
                return Ok(response);
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
