using BCrypt.Net;
using VagueVault.Backend.Helpers.Auth.Interface;

namespace VagueVault.Backend.Helpers.Auth.Implementations
{
    public class PasswordHasher:IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string password,string hashedpassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedpassword);
        }
    }
}
