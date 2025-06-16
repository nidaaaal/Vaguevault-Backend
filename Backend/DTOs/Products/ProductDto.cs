using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VagueVault.Backend.Models.Products;

namespace VagueVault.Backend.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = ""; // Initialize with default

        public decimal Price { get; set; }

        public string Description { get; set; } = "";

        public string Color { get; set; } = "";

        public int Stock { get; set; } = 10;

        public int CategoryId { get; set; }

        public int StatusId { get; set; } = 1;

        public string ImageUrl { get; set; } = ""; // Initialize

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
