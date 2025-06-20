using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Product;

namespace VagueVault.Backend.Models.Wishlists
{
    public class Wishlist
    {
      public int Id { get; set; }   

        public int ProductId { get; set; }
        public Products Products { get; set; }


        public Guid UserId { get; set; }

        public Users users { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    }
}
