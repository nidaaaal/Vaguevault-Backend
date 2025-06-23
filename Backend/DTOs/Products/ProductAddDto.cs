using System.ComponentModel.DataAnnotations.Schema;

namespace VagueVault.Backend.DTOs.Products
{
    public class ProductAddDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = ""; // Initialize with default

        public decimal Price { get; set; }

        public string Description { get; set; } = "";

        public string Color { get; set; } = "";

        public int Stock { get; set; } = 10;

        public int CategoryId { get; set; }

        public int StatusId { get; set; } = 1;


        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
