using eCommerce.Core.Entities;
using eCommerce.Core.Entities.DTO;
using eCommerce.Core.IServiceContracts;
using eCommerce.Core.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace eCommerce.Core.Services
{
    internal class UserService : IUsersService
    {
        private readonly IUserRepository _usersRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
        {
            ApplicationUser? user =
                await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return null;
            }

            // Return AuthenticationResponse with success and token
            return _mapper.Map<AuthenticationResponse>(user) with { Success = true, Token = "token" };
        }

        public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
        {
            // Create ApplicationUser object from RegisterRequest using AutoMapper
            ApplicationUser user = _mapper.Map<ApplicationUser>(registerRequest);
            ApplicationUser? registeredUser = await _usersRepository.AddUser(user);

            if (registeredUser == null)
            {
                return null;
            }

            // Return AuthenticationResponse with success and token
            return _mapper.Map<AuthenticationResponse>(registeredUser) with { Success = true, Token = "token" };
        }
    }

}