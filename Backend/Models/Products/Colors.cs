using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;

namespace VagueVault.Backend.Models.Products
{
    public class Colors
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<ProductVariants> Variants { get; set; }  
    }
}
