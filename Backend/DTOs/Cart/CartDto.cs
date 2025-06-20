namespace VagueVault.Backend.DTOs.Cart
{
    public class CartDto
    {
        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime LastUpdatedAt { get; set; }

        public List<CartItemDto> CartItemDto { get; set; }= new();
    }
}
