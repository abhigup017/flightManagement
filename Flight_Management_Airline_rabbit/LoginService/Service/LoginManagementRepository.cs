using LoginService.Enums;
using LoginService.Interface;
using LoginService.Models;
using LoginService.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoginService.Service
{
    public class LoginManagementRepository : ILoginManagementRepository
    {
        private readonly IConfiguration _configuration;
        private readonly FlightManagementContext _dbContext;

        public LoginManagementRepository(IConfiguration configuration, FlightManagementContext flightManagementContext)
        {
            _configuration = configuration;
            _dbContext = flightManagementContext;
        }

        /// <summary>
        /// Authenticate the user credentials and generate a JWT token
        /// </summary>
        /// <param name="login"></param>
        /// <returns>JWT Token</returns>
        #region Authenticate Admin
        public Token AuthenticateAdmin(Login login)
        {
            Dictionary<string, string> users = new Dictionary<string, string>();

            try
            {
                users = _dbContext.Users.Where(x => x.UserName.ToLower() == login.UserName.ToLower() && x.Password == login.Password).ToList().ToDictionary(x => x.UserName.ToLower(), x => x.RoleId.ToString());

                if (users.Count == 0)
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, login.UserName.ToLower()),
                    new Claim(ClaimTypes.Role, users[login.UserName.ToLower()])
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)

                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new Token { role = users[login.UserName.ToLower()] };
            }
            catch(Exception ex)
            {
                throw new Exception("User could not be Authenticated!!");
            }
        }
        #endregion

        #region Register User
        /// <summary>
        /// Registers a user with the user role
        /// </summary>
        /// <param name="userRegistrationRequest"></param>
        /// <returns>A boolean flag indicating if the user is registered or not.</returns>
        public bool RegisterUser(UserRegistrationRequest userRegistrationRequest)
        {
            bool isRegistered = false;
            StringBuilder errorMessage = new StringBuilder();
            try
            {
                if(_dbContext.Users.Any(x => x.EmailId.ToLower() == userRegistrationRequest.EmailId.ToLower()))
                {
                    errorMessage.Append("Email-Id is already registered in our system.\n");
                }
                if(_dbContext.Users.Any(x => x.UserName.ToLower() == userRegistrationRequest.UserName.ToLower()))
                {
                    errorMessage.Append("User name is already taken.");
                }

                if(!string.IsNullOrEmpty(errorMessage.ToString()))
                {
                    throw new Exception(errorMessage.ToString());
                }

                User user = new User
                {
                    FirstName = userRegistrationRequest.FirstName,
                    LastName = userRegistrationRequest.LastName,
                    GenderId = userRegistrationRequest.GenderId,
                    EmailId = userRegistrationRequest.EmailId,
                    RoleId = 2,
                    UserName = userRegistrationRequest.UserName,
                    Password = userRegistrationRequest.Password
                };

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                isRegistered = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return isRegistered;
        }
        #endregion
    }
}
