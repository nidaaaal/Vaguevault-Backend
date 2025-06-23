using PayPalCheckoutSdk.Core;
using System.Runtime.CompilerServices;

namespace VagueVault.Backend.Configurations
{
    public static class PayPalExtension
    {
        public static IServiceCollection AddPayPal(this IServiceCollection services,IConfiguration configuration)
        {
            var clientId = configuration["PayPal:ClientId"];
            var secret = configuration["PayPal:Secret"];
            var environment = configuration["PayPal:Environment"];

            PayPalEnvironment payPalEnvironment = environment == "live"
                ? new LiveEnvironment(clientId, secret)
                : new SandboxEnvironment(clientId, secret);

            var payPalClient = new PayPalHttpClient(payPalEnvironment);

            services.AddSingleton(payPalClient);

            return services;


        }
    }
}
