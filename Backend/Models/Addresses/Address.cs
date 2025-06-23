using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Order;

namespace VagueVault.Backend.Models.Addresses
{
    public class Address
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }    
        public Users Users { get; set; }

        public string FullName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string PostalCode { get; set; } = "";

        public ICollection<Orders> Orders { get; set; } 

    }
}
