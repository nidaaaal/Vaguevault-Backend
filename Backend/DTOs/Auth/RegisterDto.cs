using System.ComponentModel.DataAnnotations;

namespace Vauguevault.Backend.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$",
            ErrorMessage = "Only Letters, Numbers, And Underscores Are Allowed")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255,ErrorMessage ="Length exceeded")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}
