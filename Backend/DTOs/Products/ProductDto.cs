using System.ComponentModel.DataAnnotations;
using VagueVault.Backend.Models.Products;

namespace VagueVault.Backend.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public int CategoryId { get; set; }
        public int StatusId { get; set; }

        public List<ProductVariantDto> Varients { get; set; }

    }
}
