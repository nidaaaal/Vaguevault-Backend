using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VagueVault.Backend.DTOs.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = "";
        public string PaymentMethod{ get; set; } = "";
        public string PaypalOrderId { get; set; }

        public string? PaymentStatus { get; set; }


        public decimal TotalAmount { get; set; }


        public List<OrderCollectionDto> Items { get; set; } = new List<OrderCollectionDto> ();
    }
}
