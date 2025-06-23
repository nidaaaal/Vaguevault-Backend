using System.ComponentModel.DataAnnotations.Schema;
using VagueVault.Backend.Models.Order;

namespace VagueVault.Backend.DTOs.Order
{
    public class OrderCollectionDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalItemPrice { get; set; }

    }
}
