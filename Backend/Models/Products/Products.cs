using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace VagueVault.Backend.Models.Products
{
    public class Products
    {
        
        public int Id { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }


        [Required]
        public int CategoryId { get; set; } 
        public Categories Categories { get; set; }

        [Required]
        public int StatusId {  get; set; }  
        public Status Status { get; set; }  


        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public ICollection<ProductVariants> Variants { get; set; }

    }
}
