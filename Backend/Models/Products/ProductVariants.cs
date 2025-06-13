using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VagueVault.Backend.Models.Products
{
    public class ProductVariants
    {
        public int Id { get; set; }

        public int ProductId { get; set; } 
        public Products Products {  get; set; }

        public int ColorId { get; set; }   
        public Colors Colors { get; set; }

        public int SizeId { get; set; }
        public Sizes Sizes { get; set; }

        [Required]
        [Range(1, 100)]
        public int Stock { get; set; } = 10;
        [Required]
        [MaxLength(255)]
        public string ImageUrl { get; set; }


    }
}
