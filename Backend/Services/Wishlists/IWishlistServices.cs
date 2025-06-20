using VagueVault.Backend.DTOs.Products;

namespace VagueVault.Backend.Services.Wishlists
{
    public interface IWishlistServices
    {
        Task<string> AddToWishlist(Guid guid, int productId);
        Task<IEnumerable<WishlistProductDto>> ViewWishlist(Guid guid);
        Task<string> RemoveFromWishlist(Guid guid, int productId);

    }
}
