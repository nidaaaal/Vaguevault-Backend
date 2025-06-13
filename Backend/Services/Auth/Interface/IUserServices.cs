using VagueVault.Backend.Models.Auth;
using Vauguevault.Backend.DTOs.Auth;

namespace VagueVault.Backend.Services.Auth.Interface
{
    public interface IUserServices
    {
        Task<Users>RegisterUsers(RegisterDto registerDto);
        Task<string>LoginUsers(LoginDto loginDto);
    }
}
