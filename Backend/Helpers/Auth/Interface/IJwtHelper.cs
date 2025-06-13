using VagueVault.Backend.Models.Auth;

namespace VagueVault.Backend.Helpers.Auth.Interface
{
    public interface IJwtHelper
    {
        string GetJwtToken(Users user);
    }
}
