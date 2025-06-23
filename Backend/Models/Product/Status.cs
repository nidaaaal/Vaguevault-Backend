using System.ComponentModel.DataAnnotations;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Order;

namespace VagueVault.Backend.Models.Product
{
    public class Status
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<Products> Products { get; set; }
        public ICollection<Users> Users { get; set; }
        public ICollection<Orders> Orders { get; set; } 

    }
}
