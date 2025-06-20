using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Auth;
using VagueVault.Backend.Helpers.Auth.Interface;
using VagueVault.Backend.Middleware;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Repositories.Interface;
using Vauguevault.Backend.DTOs.Auth;

namespace VagueVault.Backend.Services.Auth
{
    public class AuthenticationServices:IAuthenticationServices
    {
        private readonly VagueVaultDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly  IPasswordValidator _passwordValidator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtHelper _jwtHelper;
        public AuthenticationServices(
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
                throw new BadRequestException("Username Already Exist");
            if (await _userRepository.GetUserByEmailAsync(registerDto.Email) != null)
                throw new BadRequestException("Email Already Exist");

            var (validation, errorMessage) = _passwordValidator.ValidatePassword(registerDto.Password);
            if (!validation)
            {
                throw new ForbiddenException($"Password Validation Failed :{errorMessage}");
            }
            
           

            var user = _mapper.Map<Users>(registerDto);  

            user.PasswordHash = _passwordHasher.HashPassword(registerDto.Password);

            return await _userRepository.CreateUserAsync(user);
            


        }
        public async Task<AuthResponseDto> LoginUsers(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.email);

            if (user == null)
            {
                throw new UnauthorizedException("Invalid credentials.");
            }

            if (user.StatusId == 6)
            {
                throw new UnauthorizedException("Account suspended. Please contact support.");
            }

            if (!_passwordHasher.VerifyPassword(loginDto.password, user.PasswordHash))
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts > 6)
                {
                    user.StatusId = 6;
                    await _context.SaveChangesAsync();
                    throw new UnauthorizedException("Account suspended due to multiple failed login attempts.");
                }

                await _context.SaveChangesAsync();
                throw new UnauthorizedException("Invalid credentials.");
            }

            user.FailedLoginAttempts = 0;
            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            string token = _jwtHelper.GetJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                StatusId = user.StatusId,
                Username = user.Username,
            };
        }

    }
}
