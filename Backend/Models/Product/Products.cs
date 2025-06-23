using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VagueVault.Backend.Models.Order;
using VagueVault.Backend.Models.Product;
using VagueVault.Backend.Models.Wishlists;


namespace VagueVault.Backend.Models.Product
{
    public class Products
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = ""; // Initialize with default

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Explicit decimal precision
        public decimal Price { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = "";

        [Required]
        [MaxLength(50)] // Add length limit for Color
        public string Color { get; set; } = "";

        [Required]
        [Range(1, 100)]
        public int Stock { get; set; } = 10;

        [Required]
        public int CategoryId { get; set; }
        public Categories Categories { get; set; } = null!; // Null-forgiving operator

        [Required]
        public int StatusId { get; set; } = 1;
        public Status Status { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        [Required]
        [MaxLength(255)] // Important for Cloudinary URLs
        public string ImageUrl { get; set; } = ""; // Initialize

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Initialize

        public ICollection<Wishlist> wishlists { get; set; }   
        public ICollection<OrderCollections> OrderCollections { get; set; }
    }
}