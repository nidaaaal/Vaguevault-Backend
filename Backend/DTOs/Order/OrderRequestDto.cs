namespace VagueVault.Backend.DTOs.Order
{
    public class OrderRequestDto
    {
        public int ShippingAddressId { get; set; }
        public int PaymentMethodId { get; set; }
    }
}
