using System.ComponentModel.DataAnnotations;

namespace VagueVault.Backend.Models.Order
{
    public class PaymentMethods
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }="";
        public bool IsActive { get; set; } = true;
       public ICollection<Orders> Orders { get; set; } = new List<Orders>();    
    }
}
