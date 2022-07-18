using DropdownDataService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DropdownDataService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/flight/dropdown")]
    [ApiController]
    public class DropdownDataController : ControllerBase
    {
        private readonly IDropdownDataManagementRepositiry _dropdownDataManagementRepositiry;
        private readonly ILogger _logger;

        public DropdownDataController(IDropdownDataManagementRepositiry dropdownDataManagementRepositiry, ILogger<DropdownDataController> logger)
        {
            _dropdownDataManagementRepositiry = dropdownDataManagementRepositiry;
            _logger = logger;
        }

        #region Get Gender types
        [HttpGet, Route("gender")]
        public IActionResult GetGenderTypes()
        {
            try
            {
                var genderTypes = _dropdownDataManagementRepositiry.GetGenderTypes();
                return Ok(genderTypes);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Get Instrument types
        [HttpGet, Route("instrument")]
        public IActionResult GetInstrumentTypes()
        {
            try
            {
                var instrumentTypes = _dropdownDataManagementRepositiry.GetInstrumentTypes();
                return Ok(instrumentTypes);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Get Locations
        [HttpGet, Route("locations")]
        public IActionResult GetLocations()
        {
            try
            {
                var locations = _dropdownDataManagementRepositiry.GetLocations();
                return Ok(locations);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Get Meal Plans
        [HttpGet, Route("mealplan")]
        public IActionResult GetMealPlanTypes()
        {
            try
            {
                var mealPlanTypes = _dropdownDataManagementRepositiry.GetMealPlanTypes();
                return Ok(mealPlanTypes);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Get Roles types
        [HttpGet, Route("roles")]
        public IActionResult GetRoleTypes()
        {
            try
            {
                var roleTypes = _dropdownDataManagementRepositiry.GetRoleTypes();
                return Ok(roleTypes);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Get Airline Data
        [HttpGet, Route("airlines")]
        public IActionResult GetAirlineDropdownData()
        {
            try
            {
                var airlines = _dropdownDataManagementRepositiry.GetAirlineDropdownData();
                return Ok(airlines);
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
