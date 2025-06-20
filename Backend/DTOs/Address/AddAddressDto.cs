using System.ComponentModel.DataAnnotations;

namespace VagueVault.Backend.DTOs.Address
{
    public class AddAddressDto
    {
        [Required]
        public string FullName { get; set; } = "";
        [Required]
        public string PhoneNumber { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Street { get; set; } = "";
        [Required]
        public string City { get; set; } = "";
        [Required]
        public string PostalCode { get; set; } = "";
    }
}
