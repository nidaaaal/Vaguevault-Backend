using System.ComponentModel.DataAnnotations;

namespace VagueVault.Backend.Models.Products
{
    public class Sizes
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<ProductVariants> Variants { get; set; }
    }
}
