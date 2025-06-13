using System.ComponentModel.DataAnnotations;
using VagueVault.Backend.Models.Products;

namespace VagueVault.Backend.DTOs.Products
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = "";          

    }
}
