using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.Helpers.Auth.Implementations;
using VagueVault.Backend.Helpers.Auth.Interface;
using VagueVault.Backend.Repositories.Implementations;
using VagueVault.Backend.Repositories.Interface;
using VagueVault.Backend.Services.Addressess;
using VagueVault.Backend.Services.Auth;
using VagueVault.Backend.Services.Cart;
using VagueVault.Backend.Services.Orderss;
using VagueVault.Backend.Services.Payment;
using VagueVault.Backend.Services.Product;
using VagueVault.Backend.Services.Statuses;
using VagueVault.Backend.Services.User;
using VagueVault.Backend.Services.Wishlists;

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
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IPaymentRepository,PaymentRepository>();
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IProductServices,ProductServices>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IUserServices, UserServices>();  
            services.AddScoped<IWishlistServices, WishlistServices>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<IAddressServices, AddressServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<ICategoryStatusServices, CategoryStatusServices>();
            services.AddScoped<IPayPalService, PayPalService>();    

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
