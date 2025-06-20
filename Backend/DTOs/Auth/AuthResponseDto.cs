namespace VagueVault.Backend.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = "";
        public Guid UserId { get; set; }
        public string Username { get; set; } = "";
        public int StatusId { get; set; }
    }
}
