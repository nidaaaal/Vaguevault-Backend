using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Repositories.Interface;

namespace VagueVault.Backend.Repositories.Implementations
{
    public class UserRepository:IUserRepository
    {
        private readonly VagueVaultDbContext _dbContext;
        public UserRepository(VagueVaultDbContext vagueVaultDbContext) 
        { 
            _dbContext = vagueVaultDbContext;   
        }

        public async Task<Users> CreateUserAsync(Users users)
        {
            _dbContext.Users.Add(users);
            await _dbContext.SaveChangesAsync();  
            return users;
        }
        public async Task<Users?> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x=>x.Username==username);
        }
        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x=>x.Email==email); 
        }
    }
}
