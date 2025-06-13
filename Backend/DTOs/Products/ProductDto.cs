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
        public Categories Categories { get; set; }
        public Status Status { get; set; }

        public List<ProductVariantDto> Varients { get; set; }

    }
}
