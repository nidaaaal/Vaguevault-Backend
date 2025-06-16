using CloudinaryDotNet;

namespace VagueVault.Backend.Configurations
{
    public static class CloudinaryExtensions
    {
        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            var cloudinaryAccount = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);

            var cloudinary = new Cloudinary(cloudinaryAccount);

            services.AddSingleton<ICloudinary>(cloudinary);
            return services ;
        }
    }
}
