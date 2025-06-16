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
        public AuthenticationController (IAuthenticationServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegisterDto registerDto)
        {
            try
            {
                var data = await _userServices.RegisterUsers(registerDto);
                if (data == null) return BadRequest(data);
                return Ok(new  {Message= " REGISTRATION COMPLITED SUCCESSFULLY !! " });
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var token = await _userServices.LoginUsers(loginDto);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
