using VagueVault.Backend.Models.Auth;

namespace VagueVault.Backend.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<Users> CreateUserAsync(Users users);
        Task<Users?> GetUserByUsernameAsync(string username);
        Task<Users?> GetUserByEmailAsync(string email);
        Task<Users?> GetUserByGuidAsync(Guid id);



    }
}
