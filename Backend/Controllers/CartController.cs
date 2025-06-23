using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VagueVault.Backend.DTOs.Cart;
using VagueVault.Backend.Services.Cart;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VagueVault.Backend.Controllers
{
    [Authorize]
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }
        [HttpGet("View")]
        public async Task<IActionResult> Get(Guid id)
        {
            var cart = await _cartServices.GetCartItems(id);
            return Ok(new { cart });
        }
        [HttpPost("addToCart")]
        public async Task<IActionResult> AddToCart(Guid id,CartItemRequestDto request)
        {
           var response = await _cartServices.AddToCart(id,request);
            if (!response)
                return BadRequest(new { Message = "Failed to Add Product to from cart." });

            return Ok(new { Message = "Product Added to cart." });
        }
        [HttpDelete("removeFromCart")]
        public async Task<IActionResult> RemoveFromCart(Guid id, int productId)
        {
            var response = await _cartServices.RemoveFromCart(id, productId);
            if (!response)
                return BadRequest(new { Message = "Failed to Remove product from cart." });

            return Ok(new { Message = "Product quantity Remove from cart." });
        }
        [HttpPatch("updateQuantity")]
        public async Task<IActionResult> DecreaseQuantity(Guid id,CartItemRequestDto requestDto)
        {
           var response  = await _cartServices.DeceaseQuantity(id, requestDto);
            if (!response)
                return BadRequest(new { Message = "Failed to decrease product quantity from cart." });

            return Ok(new { Message = "Product quantity decreased from cart." });
        }
        [HttpDelete("clearCart")]
        public async Task<IActionResult> ClearCart(Guid id)
        {
            var response = await _cartServices.ClearCart(id);
            if (!response)
                return BadRequest(new { Message = "Failed to ClearCart." });

            return Ok(new { Message = "Cart Cleared" });
        }


    }
}
