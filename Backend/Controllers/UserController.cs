using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VagueVault.Backend.Services.User;

namespace VagueVault.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
                _userServices = userServices;   
        }

        [Authorize(Policy ="AdminOnly")]
        [HttpGet("Get_all")]
        public async Task<IActionResult> GetAll()
        {
           var Users = await _userServices.GetAllAsync();
            return Ok(new { Users });
        }

        [HttpGet("Get_By_Username")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _userServices.GetByUsernameAsync(username);
            return Ok(new { user });
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("Change_status")]
        public async Task<IActionResult> ChangeStatus(string username,int statusid)
        {
            var Response = await _userServices.ChangeStatusAsync(username, statusid);
            return Ok(new { Response });
        }
        [Authorize(Roles ="User")]
        [HttpPut("Change_password")]
        public async Task<IActionResult> ChangePassword (string email, string currentPassword, string newPassword)
        {
            var Response = await _userServices.ChangePasswordAsync(email, currentPassword, newPassword);
            return Ok(new { Response });

        }
        [Authorize(Roles = "User")]
        [HttpPut("Change_username")]
        public async Task<IActionResult> ChangeUsername(string username, string email, string newUsername)
        {
           var Response = await _userServices.ChangeUsernameAsync(username, email, newUsername);
            return Ok(new { Response });


        }




    }
}
