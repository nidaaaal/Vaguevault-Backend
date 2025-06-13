using System.ComponentModel.DataAnnotations;

namespace VagueVault.Backend.Models.Products
{
    public class Categories
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }   
        
        public ICollection<Products> Products { get; set; }
    }
}
