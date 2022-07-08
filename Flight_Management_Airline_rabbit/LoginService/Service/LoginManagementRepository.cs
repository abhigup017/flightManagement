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
                users = _dbContext.Users.Where(x => x.RoleId == (int)UserRoles.Admin && x.UserName == login.UserName && x.Password == login.Password).ToList().ToDictionary(x => x.UserName, x => x.Password);

                if (users.Count == 0)
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, login.UserName),
                    new Claim(ClaimTypes.Role, UserRoles.Admin.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(40),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)

                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new Token { token = tokenHandler.WriteToken(token) };
            }
            catch(Exception ex)
            {
                throw new Exception("User could not be Authenticated!!");
            }
        }
        #endregion
    }
}
