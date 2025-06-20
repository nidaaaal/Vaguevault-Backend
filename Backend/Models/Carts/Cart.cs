
using VagueVault.Backend.Models.Auth;

namespace VagueVault.Backend.Models.Carts
{
    public class Cart
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public Users user { get; set; } = null !;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime LastUpdatedAt { get; set; }

        public ICollection<CartItems> Items { get; set; }
    }
}
