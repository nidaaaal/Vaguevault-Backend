
namespace VagueVault.Backend.Services.Payment
{
    public interface IPayPalService
    {
        Task<string> CreateOrder(int orderId);
        Task<string> CaptureOrder(String paypalOrderId);

    }
}
