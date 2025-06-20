using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Product;

namespace VagueVault.Backend.Models.Carts
{
    public class CartItems
    {
        public int Id { get; set; }
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public Products Product { get; set; } = null!;

        public Cart Carts { get; set; } = null!;

            
        
    }
}
