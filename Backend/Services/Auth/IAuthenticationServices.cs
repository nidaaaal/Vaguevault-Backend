using VagueVault.Backend.DTOs.Auth;
using VagueVault.Backend.Models.Auth;
using Vauguevault.Backend.DTOs.Auth;

namespace VagueVault.Backend.Services.Auth
{
    public interface IAuthenticationServices
    {
        Task<Users>RegisterUsers(RegisterDto registerDto);
        Task<AuthResponseDto> LoginUsers(LoginDto loginDto);
    }
}
