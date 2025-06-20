using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VagueVault.Backend.DTOs.Address;
using VagueVault.Backend.Services.Addressess;

namespace VagueVault.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServices _addressServices;

        public AddressController(IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }
        [HttpGet("Get-Address")]
        public async Task<IActionResult> Get(Guid id)
        {
            var Address = await _addressServices.GetAddress(id);
            return Ok(new { Address });

        }

        [HttpPost("Add-Address")]
        public async Task<IActionResult> Create(Guid id,[FromForm]AddAddressDto addressDto)
        {
            var Address = await _addressServices.AddAddress(id, addressDto);
            return Ok(new { Address });

        }
        [HttpPut("Update-Address")]
        public async Task<IActionResult> Update(Guid id,[FromForm] AddressDto addressDto)
        {
            var Address = await _addressServices.UpdateAddress(id, addressDto);
            return Ok(new { Address });
        }
        [HttpDelete("Delete-Address")]
        public async Task<IActionResult> Delete(Guid id,int addressId)
        {
            var Response = await _addressServices.RemoveAddress(id,addressId);
            return Ok(new { Message = "address Removed!", Response });
        }


    }
}
