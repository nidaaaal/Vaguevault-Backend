using VagueVault.Backend.DTOs.Users;
using VagueVault.Backend.Models.Auth;

namespace VagueVault.Backend.Services.User
{
    public interface IUserServices
    {
        Task<ICollection<UserDto>> GetAllAsync();
        Task<UserDto?> GetByUsernameAsync(string username);
        Task<string> ChangeStatusAsync(string username, int statusId);
        Task<string> ChangePasswordAsync(string email, string currentPassword, string newPassword);
        Task<string> ChangeUsernameAsync(string username,string email,string newUsername);
        
    }
}
