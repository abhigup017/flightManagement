using DiscountService.Interface;
using DiscountService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/flight/discount")]
    [ApiController]
    [Authorize]
    public class DiscountController : ControllerBase
    {
        private IDiscountManagementRepository _discountManagementRepository;
        private ILogger _logger;

        public DiscountController(IDiscountManagementRepository discountManagementRepository, ILogger<DiscountController> logger)
        {
            _discountManagementRepository = discountManagementRepository;
            _logger = logger;
        }

        #region Add Discount
        [HttpPost, Route("add")]
        public IActionResult AddDiscount(AddDiscountRequest addDiscountRequest)
        {
            try
            {
                bool isAdded = _discountManagementRepository.AddDiscount(addDiscountRequest);
                return Ok(new { isAdded = isAdded });
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, new { errorMessage = ex.Message});
            }
        }
        #endregion

        #region Delete discount
        [HttpDelete, Route("delete/{discountId}")]
        public IActionResult DeleteDiscount(int discountId)
        {
            try
            {
                bool isDeleted = _discountManagementRepository.DeleteDiscount(discountId);
                return Ok(new { isDeleted = isDeleted });
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Get all Discounts
        [HttpGet, Route("all")]
        public IActionResult GetAllDiscount()
        {
            try
            {
                var response = _discountManagementRepository.GetAllDiscount();
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Validate Discount code
        [HttpGet, Route("code/{discountCode}")]
        public IActionResult ValidateDiscountCode(string discountCode)
        {
            try
            {
                int discountValue = _discountManagementRepository.ValidateDiscountCode(discountCode);
                return Ok(new { discountValue = discountValue });
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return StatusCode(500, new { errorMessage = ex.Message });
            }
        }
        #endregion
    }
}
