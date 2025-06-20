using VagueVault.Backend.DTOs.Cart;

namespace VagueVault.Backend.Services.Cart
{
    public interface ICartServices
    {
        Task<IEnumerable<CartItemDto>?> GetCartItems(Guid id);
        Task<bool> AddToCart(Guid id,CartItemRequestDto requestDto);
        Task<bool> RemoveFromCart(Guid id, int productId);
        Task<bool>   DeceaseQuantity(Guid id, CartItemRequestDto cartItem);
        Task <bool> ClearCart(Guid id);



    }
}
