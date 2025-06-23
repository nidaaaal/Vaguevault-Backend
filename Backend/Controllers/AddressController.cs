using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VagueVault.Backend.DTOs.Address;
using VagueVault.Backend.Services.Addressess;

namespace VagueVault.Backend.Controllers
{
    [Authorize]
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServices _addressServices;

        public AddressController(IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }
        [HttpGet("View")]
        public async Task<IActionResult> Get(Guid id)
        {
            var Address = await _addressServices.GetAddress(id);
            return Ok(new { Address });

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Guid id,[FromForm]AddAddressDto addressDto)
        {
            var AddedAddress = await _addressServices.AddAddress(id, addressDto);
            return Ok(new { AddedAddress });

        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Guid id,[FromForm] AddressDto addressDto)
        {
            var UpdatetdAddress = await _addressServices.UpdateAddress(id, addressDto);
            return Ok(new { UpdatetdAddress });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id,int addressId)
        {
            var Response = await _addressServices.RemoveAddress(id,addressId);
            return Ok(new { Message = "address Removed!", Response });
        }


    }
}
