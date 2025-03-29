using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Entities.DTO;

namespace eCommerce.Core.IServiceContracts
{
    public interface IUsersService
    {
       Task<AuthenticationResponse?> Login(LoginRequest? loginRequest);

       Task<AuthenticationResponse?> Register(RegisterRequest? registerRequest);
    }
}
