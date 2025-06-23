using VagueVault.Backend.DTOs.Products;

namespace VagueVault.Backend.DTOs.Cart
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";

        public decimal Price { get; set; }  

        public int Quantity { get; set; }

        public string ImageUrl { get; set; } = "";

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public ProductDto productDto { get; set; }
    }
}
