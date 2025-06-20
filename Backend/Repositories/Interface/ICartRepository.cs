using VagueVault.Backend.DTOs.Cart;
using VagueVault.Backend.Models.Carts;

namespace VagueVault.Backend.Repositories.Interface
{
    public interface ICartRepository
    {
        Task<Cart?> GetCart(Guid id);
    }
}
