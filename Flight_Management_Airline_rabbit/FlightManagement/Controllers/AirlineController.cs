using AirlineService.Interfaces;
using AirlineService.ViewModels;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/flight/airline")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineManagementRepository _airlineManagementRepository;
        private readonly ILogger _logger;
        public AirlineController(IAirlineManagementRepository airlineManagementRepository, ILogger<AirlineController> logger)
        {
            _airlineManagementRepository = airlineManagementRepository;
            _logger = logger;
        }
        #region Register Airline
        [HttpPost, Route("register")]
        public IActionResult RegisterAirline(AirlineRegistrationRequest airlineDetails)
        {
            try
            {
                _airlineManagementRepository.RegisterAirline(airlineDetails);
                //return StatusCode(201, new { insertedId = response });
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Add Airline Inventory
        [HttpPost, Route("inventory/add")]
        public IActionResult AddAirlineInventory(AirlineInventorySchedule airlineInventorySchedule)
        {
            bool response = default;
            try
            {
                response = _airlineManagementRepository.AddAirlineInventory(airlineInventorySchedule);
                return Ok(new { isInserted = response });
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, new { isInserted = response });
            }
        }
        #endregion

        #region Block Airline
        [HttpPut, Route("block/airline/{airlineId}")]
        public IActionResult BlockAirline(int airlineId)
        {
            try
            {
                var response = _airlineManagementRepository.BlockAirline(airlineId);
                return Ok(new { isBlocked = response });
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
