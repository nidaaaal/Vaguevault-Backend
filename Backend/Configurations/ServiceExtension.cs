using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.Helpers.Auth.Implementations;
using VagueVault.Backend.Helpers.Auth.Interface;
using VagueVault.Backend.Repositories.Implementations;
using VagueVault.Backend.Repositories.Interface;
using VagueVault.Backend.Services.Auth.Implementations;
using VagueVault.Backend.Services.Auth.Interface;
using VagueVault.Backend.Services.Product.Implimentations;
using VagueVault.Backend.Services.Product.Interface;

namespace VagueVault.Backend.Configurations
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<VagueVaultDbContext>(option => 
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IProductServices,ProductServices>();
        }

        public static void ConfigureHelpers(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IJwtHelper, JwtHelper>();
            services.AddSingleton<IPasswordHasher,PasswordHasher>();   
            services.AddSingleton<IPasswordValidator, PasswordValidator>();    
        }
        public static void ConfigureMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DtoMapper));
        }

       

    }
}
