using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VagueVault.Backend.Services.Wishlists;

namespace VagueVault.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistServices _wishlist;

        public WishlistController(IWishlistServices wishlist)
        {
            _wishlist = wishlist;
        }
        [Authorize(Roles = "User")]
        [HttpPost] 
        public async Task<IActionResult> AddToWishlist(Guid guid, int productId)
        {
           var Status = await  _wishlist.AddToWishlist(guid, productId);   
            return Ok(new { Status });
        }

        [HttpGet]
        public async Task<IActionResult> ViewWishlist(Guid guid)
        {
           var Wishlist = await _wishlist.ViewWishlist(guid);
            return Ok(new {Wishlist});
        }

        [Authorize(Roles ="User")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFromWishlist(Guid guid, int productId)
        {
           var Status = await  _wishlist.RemoveFromWishlist(guid, productId);
            return Ok(new {Status});
        }


    }
}
