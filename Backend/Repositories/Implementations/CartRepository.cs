using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Cart;
using VagueVault.Backend.Models.Carts;
using VagueVault.Backend.Repositories.Interface;

namespace VagueVault.Backend.Repositories.Implementations
{
    public class CartRepository:ICartRepository
    {
        private readonly VagueVaultDbContext _context;
        public CartRepository(VagueVaultDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCart(Guid id)
        {
         return await _context.Cart
                .Include(c => c.Items)
                .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

    }
}
