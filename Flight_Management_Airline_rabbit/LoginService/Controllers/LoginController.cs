using LoginService.Interface;
using LoginService.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginService.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/flight/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManagementRepository _loginManagementRepository;
        private readonly ILogger _logger;

        public LoginController(ILoginManagementRepository loginManagementRepository, ILogger<LoginController> logger)
        {
            _loginManagementRepository = loginManagementRepository;
            _logger = logger;
        }

        #region Admin Login
        [HttpPost, Route("admin")]
        public IActionResult AuthenticateAdmin(Login login)
        {
            try
            {
                var token = _loginManagementRepository.AuthenticateAdmin(login);

                if (token == null)
                    return Unauthorized();

                return Ok(token);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
                return Unauthorized(ex.Message);
            }
        }
        #endregion

        #region Register User
        [HttpPost, Route("user/register")]
        public IActionResult RegisterUser(UserRegistrationRequest userRegistrationRequest)
        {
            try
            {
                bool isRegistered = _loginManagementRepository.RegisterUser(userRegistrationRequest);
                return Ok(new { isRegistered = isRegistered });
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
