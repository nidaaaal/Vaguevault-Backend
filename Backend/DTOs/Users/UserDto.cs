namespace VagueVault.Backend.DTOs.Users
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public int FailedLoginAttempts { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime? LastLogin { get; set; } 

        public DateTime? PasswordChangedAt { get; set; }

        public DateTime? UsernameChangedAt { get; set; }

        public int StatusId { get; set; }


        public string Role { get; set; } = "";

    }
}
