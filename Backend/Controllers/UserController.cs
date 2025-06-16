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

        [HttpGet("Get_all")]
        public async Task<IActionResult> GetAll()
        {
           var data = await _userServices.GetAllAsync();
            return Ok(data);    
        }
        [HttpGet("Get_By_Username")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var data = await _userServices.GetByUsernameAsync(username);
            return data == null ? NotFound(data):Ok(data);
        }
        [HttpPut("Change_status")]
        public async Task<IActionResult> ChangeStatus(string username,int statusid)
        {
            var data = await _userServices.ChangeStatusAsync(username, statusid);
            return data == null ? NotFound(data) : Ok(data);
        }
        [HttpPut("Change_password")]
        public async Task<IActionResult> ChangePassword (string email, string currentPassword, string newPassword)
        {
            var data = await _userServices.ChangePasswordAsync(email, currentPassword, newPassword);
            return data == null ? NotFound(data) : Ok(data);

        }
        [HttpPut("Change_username")]
        public async Task<IActionResult> ChangeUsername(string username, string email, string newUsername)
        {
           var data = await _userServices.ChangeUsernameAsync(username, email, newUsername);
           return data == null ? NotFound(data) : Ok(data);


        }




    }
}
