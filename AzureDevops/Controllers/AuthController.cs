﻿using eCommerce.Core.Entities.DTO;
using eCommerce.Core.IServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevops.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUsersService _usersService;

        public AuthController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest("Invalid registration data");
            }

           AuthenticationResponse? authenticationResponse =  await _usersService.Register(registerRequest);

           if (authenticationResponse == null || authenticationResponse.Success == false)
           {
               return BadRequest(authenticationResponse);
           }

           return Ok(authenticationResponse);

        }


        [HttpPost("login")]
        public async Task<IActionResult?> Login(LoginRequest loginRequest)
        {

            if (loginRequest == null)
            {
                return BadRequest("Invalid login data");
            }

            AuthenticationResponse? authenticationResponse = await _usersService.Login(loginRequest);

            if (authenticationResponse == null || authenticationResponse.Success == false)
            {
                return Unauthorized(authenticationResponse);
            }

            return Ok(authenticationResponse);
        }
    }
}
