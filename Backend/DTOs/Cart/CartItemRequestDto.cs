using System.ComponentModel.DataAnnotations;

namespace VagueVault.Backend.DTOs.Cart
{
    public class CartItemRequestDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
    }
}
