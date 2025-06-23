using System.ComponentModel.DataAnnotations.Schema;
using VagueVault.Backend.Models.Product;

namespace VagueVault.Backend.Models.Order
{
    public class OrderCollections
    {
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        public Orders Orders { get; set; }
        
        public int ProductId { get; set; }
        public Products Products { get; set; }

        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")] // Explicit decimal precision

        public decimal Price {  get; set; }
        [Column(TypeName = "decimal(18,2)")] // Explicit decimal precision

        public decimal TotalItemPrice { get; set; } 
    }

}