using System.ComponentModel.DataAnnotations;
using VagueVault.Backend.Models.Auth;

namespace VagueVault.Backend.Models.Products
{
    public class Status
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<Products> Products { get; set; }
        public ICollection<Users> Users { get; set; }

    }
}
