using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Users;
using VagueVault.Backend.Helpers.Auth.Interface;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Products;
using VagueVault.Backend.Repositories.Interface;
using Vauguevault.Backend.DTOs.Auth;

namespace VagueVault.Backend.Services.User
{
    public class UserServices:IUserServices
    {
        private readonly VagueVaultDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordValidator _validator;
        private readonly IPasswordHasher _hasher;

        public UserServices(VagueVaultDbContext vagueVaultDb,IUserRepository repository, 
            IPasswordHasher passwordHasher,IPasswordValidator passwordValidator,IMapper mapper) 
        { 
                _context = vagueVaultDb;
            _userRepository = repository;
            _hasher = passwordHasher;
            _validator = passwordValidator;
            _mapper = mapper;
        }


       public async Task<ICollection<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            var userDtos =_mapper.Map<List<UserDto>>(users);
            return userDtos;
        }
        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
           var data = await _userRepository.GetUserByUsernameAsync(username);
           if(data == null) return null;
            return _mapper.Map<UserDto>(data);
        }
        public async Task<string> ChangeStatusAsync(string username, int statusId)
        {
           var data = await _userRepository.GetUserByUsernameAsync(username);
            if (data == null) return "No User found";
            if ( !(await _context.Status.AnyAsync(x => x.Id == statusId))) return "Invalid StatusId";
            data.StatusId = statusId;
           await _context.SaveChangesAsync();

            return $"User Status Changed";


        }
        public async Task<string> ChangePasswordAsync(string email, string currentPassword,string newPassword)
        {
           var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) throw new InvalidOperationException("USER DOESN'T EXIST");


            if (user == null || !_hasher.VerifyPassword(currentPassword, user.PasswordHash))
            {
                throw new InvalidOperationException("Older password Does not match with current password");
            }

            if (user.PasswordChangedAt.HasValue &&
       user.PasswordChangedAt.Value.AddDays(30) > DateTime.UtcNow)
            {
                var nextAllowedChangeDate = user.PasswordChangedAt.Value.AddDays(30);
                throw new SecurityException($"You can only change your password once per month. Next allowed change: {nextAllowedChangeDate:yyyy-MM-dd}");
            }


            var (valid, errorMessage) = _validator.ValidatePassword(newPassword);

            if (!valid)
            {
                throw new InvalidOperationException($"New Password Validation failed : {errorMessage}");
            }


            user.PasswordHash = _hasher.HashPassword(newPassword);

            if (user.PasswordHash == currentPassword) throw new InvalidOperationException("new password cannot be same as old password!");

            user.PasswordChangedAt = DateTime.Now;
            await _context.SaveChangesAsync();  
            return "Password Changed Successfully";

        }
        public async Task<string> ChangeUsernameAsync(string username,string email, string newUsername)
        {
           var user = await _userRepository.GetUserByUsernameAsync(username);
           if (user == null) throw new InvalidOperationException("USER DOESN'T EXIST");

            if (user.Email != email)
                throw new InvalidOperationException("Email Not Matching");

            if (await _userRepository.GetUserByUsernameAsync(newUsername) != null)
                throw new InvalidOperationException("Username Already Exist");

            user.Username = newUsername;
            await _context.SaveChangesAsync();

            return "Username Updated";

        }

    }
}
