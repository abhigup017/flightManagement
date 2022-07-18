using LoginService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginService.Interface
{
    public interface ILoginManagementRepository
    {
        Token AuthenticateAdmin(Login login);
        bool RegisterUser(UserRegistrationRequest userRegistrationRequest);
    }
}
