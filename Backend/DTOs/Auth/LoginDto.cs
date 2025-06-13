using System.ComponentModel.DataAnnotations;

namespace Vauguevault.Backend.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public string email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]   
        public string password { get; set; } =string.Empty;
    }
}
