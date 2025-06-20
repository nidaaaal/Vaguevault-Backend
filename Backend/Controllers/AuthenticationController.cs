using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using VagueVault.Backend.Helpers.Auth.Interface;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Services.Auth;
using Vauguevault.Backend.DTOs.Auth;

namespace VagueVault.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _userServices;
        public AuthenticationController(IAuthenticationServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegisterDto registerDto)
        {

            var data = await _userServices.RegisterUsers(registerDto);
            if (data == null) return BadRequest(data);
            return Ok(new { Message = " REGISTRATION COMPLITED SUCCESSFULLY !! " });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            var user = await _userServices.LoginUsers(loginDto);

            return Ok(new { user });

        }
    }
}
