using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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

        public string Role { get; set; } = "User";

        public string Status { get; set; } = "Active"; 

        public static class Roles
        {
            public const string Admin = "Admin";    

            public const string User = "User";  
        } 

        public static class Statuses
        {
            public const string Active= "Active";
            public const string Inactive = "Inactive";
            public const string Suspended= "Suspended";
        }
    }
}
