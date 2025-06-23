using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VagueVault.Backend.Models.Addresses;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Product;

namespace VagueVault.Backend.Models.Order
{
    public class Orders
    {
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public Users? Users { get; set; }
        public DateTime OrderDate { get; set; }=DateTime.Now;   

        [Required]
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        [Required]
        public int ShippingAddressId { get; set; }
        public Address? Address { get; set; }
        [Required]
        public int PaymentMethodId { get; set; }
        public PaymentMethods? PaymentMethods { get; set; }

        [Column(TypeName = "decimal(18,2)")] // Explicit decimal precision
        public decimal TotalAmount { get; set; }

        public string? PaypalOrderId { get; set; }

        public string? PaymentStatus { get; set; } = "PENDING";



        public ICollection<OrderCollections> OrderCollections { get; set; } = new List<OrderCollections>(); 

    }
}
    