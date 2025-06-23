using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using VagueVault.Backend.Models.Wishlists;

using VagueVault.Backend.Models.Product;
using VagueVault.Backend.Models.Carts;
using VagueVault.Backend.Models.Addresses;
using VagueVault.Backend.Models.Order;

namespace VagueVault.Backend.Models.Auth
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public int FailedLoginAttempts { get; set; }    

        public DateTime createdAt { get; set; }= DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }

        public DateTime? PasswordChangedAt { get; set; }

        public DateTime? UsernameChangedAt { get; set; }

        public string Role { get; set; } = "User";

        public int StatusId { get; set; } = 1;
        public Status Status { get; set; } = null!;

        public ICollection<Wishlist> wishlists { get; set; }
        public ICollection<Address> Address { get; set; }
        public ICollection<Orders> Orders { get; set; } 

        public Cart Cart { get; set; }

        public static class Roles
        {
            public const string Admin = "Admin";    

            public const string User = "User";  
        } 

    }
}
