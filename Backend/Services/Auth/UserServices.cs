using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security;
using VagueVault.Backend.Data;
using VagueVault.Backend.Helpers.Auth.Interface;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Repositories.Interface;
using Vauguevault.Backend.DTOs.Auth;

namespace VagueVault.Backend.Services.Auth
{
    public class UserServices:IUserServices
    {
        private readonly VagueVaultDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly  IPasswordValidator _passwordValidator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtHelper _jwtHelper;
        public UserServices(
            IMapper mapper,IUserRepository repository,IPasswordHasher passwordHasher,IPasswordValidator passwordValidator,
            VagueVaultDbContext vagueVaultDbContext,IJwtHelper jwtHelper) 
        {
            _mapper = mapper;
            _userRepository = repository;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _context = vagueVaultDbContext;
            _jwtHelper = jwtHelper;

        }

        public async Task<Users> RegisterUsers(RegisterDto registerDto)
        {
            if (await _userRepository.GetUserByUsernameAsync(registerDto.Username) != null)
                throw new InvalidOperationException("Username Already Exist");
            if (await _userRepository.GetUserByEmailAsync(registerDto.Email) != null)
                throw new InvalidOperationException("Email Already Exist");

            var (validation, errorMessage) = _passwordValidator.ValidatePassword(registerDto.Password);
            if (!validation)
            {
                throw new SecurityException($"Password Validation Failed :{errorMessage}");
            }
            
           

            var user = _mapper.Map<Users>(registerDto);  

            user.PasswordHash = _passwordHasher.HashPassword(registerDto.Password);

            return await _userRepository.CreateUserAsync(user);
            


        }
        public async Task<string> LoginUsers(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.email) ;
            if(user==null || ! _passwordHasher.VerifyPassword(loginDto.password, user.PasswordHash))
            {
                if (user != null)
                {
                    user.FailedLoginAttempts++;
                    await _context.SaveChangesAsync();

                    if (user.FailedLoginAttempts > 5)
                    {
                        user.Status=Users.Statuses.Suspended;
                        throw new Exception("Account suspended. Please contact support");

                    }
                    throw new Exception($"After {5-user.FailedLoginAttempts} try your Account will be suspender!");
                }
                throw new Exception("Invalid username or password");
            }
            user.FailedLoginAttempts = 0;
            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return _jwtHelper.GetJwtToken(user);  
        }
    }
}
