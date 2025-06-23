using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VagueVault.Backend.DTOs.Order;
using VagueVault.Backend.Services.Orderss;

namespace VagueVault.Backend.Controllers
{
    [Authorize]
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("allUsers")]
        public async Task<IActionResult> GetAll()
        {
            var UsersOrders = await _orderServices.ViewUserOrders();
            return Ok(new { UsersOrders });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var Orders = await _orderServices.ViewOrders(id);
            return Ok(new { Orders });
        }

        [HttpPost("placeOrder")]
        public async Task<IActionResult> PlaceOrder(Guid id,[FromForm] OrderRequestDto orderDto)
        {
            var OrderId = await _orderServices.PlaceOrder(id, orderDto);
            return Ok(new { OrderId });
        }
    }
}
