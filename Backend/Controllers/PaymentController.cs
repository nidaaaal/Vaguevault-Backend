using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VagueVault.Backend.Data;
using VagueVault.Backend.Services.Payment;
using VagueVault.Backend.Middleware;
using Microsoft.EntityFrameworkCore;

namespace VagueVault.Backend.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayPalService _payPalService;

        public PaymentController(IPayPalService payPalService,VagueVaultDbContext vagueVaultDb)
        {
            _payPalService = payPalService; 
        }

        [Authorize(Roles ="User")]
        [HttpPost("start/{orderId}")]
        public async Task<IActionResult> Start(int orderId)
        {
         var url =await   _payPalService.CreateOrder(orderId);
            return Ok(new {url});   
        }

        [Authorize(Roles = "User")]
        [HttpPost("return")]
        public async Task<IActionResult> Confirm(string token)
        {
            var PaymentStatus = await _payPalService.CaptureOrder(token);
            if (PaymentStatus==null) throw new BadRequestException("Payment Failed");

            return Ok(new { PaymentStatus });
        }
    }
}
