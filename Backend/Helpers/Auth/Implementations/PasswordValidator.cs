using System.Text.RegularExpressions;
using VagueVault.Backend.Helpers.Auth.Interface;

namespace VagueVault.Backend.Helpers.Auth.Implementations
{

    public class PasswordValidator : IPasswordValidator
    {
        public (bool isValid, string message) ValidatePassword(string password,string username  )
        {
            if (string.IsNullOrWhiteSpace(password))
                return (false, "Password cannot be empty");

            if (password.Length < 12)
                return (false, "Password must be at least 12 characters long");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return (false, "Password must contain at least one uppercase letter");

            if (!Regex.IsMatch(password, @"[a-z]"))
                return (false, "Password must contain at least one lowercase letter");

            if (!Regex.IsMatch(password, @"[0-9]"))
                return (false, "Password must contain at least one number");

            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
                return (false, "Password must contain at least one special character");

            // Check for common passwords
            if (IsCommonPassword(password, username))
                return (false, "Password is too common. Choose a more unique password");

            return (true, "Password is valid");

        }

        private bool IsCommonPassword(string password,string username)
        {
            var commonPasswords = new List<string>
        {
            "password", "123456", "12345678", "123456789", "12345","98765","987654321",
            "qwerty", "abc123", "password1", "admin", "welcome",$"{username}"
        };

            return commonPasswords.Contains(password.ToLower());
        }

    }
    
    }
