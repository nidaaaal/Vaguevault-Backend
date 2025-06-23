namespace VagueVault.Backend.DTOs.Address
{
    public class AddressDto
    {
        public int Id { get; set; } 
        public string FullName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string PostalCode { get; set; } = "";
    }
}
