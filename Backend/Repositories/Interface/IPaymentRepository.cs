
namespace VagueVault.Backend.Repositories.Interface
{
    public interface IPaymentRepository
    {
        Task<bool> UpdateStock(int id,int count);
        decimal ConvertINRtoUSD(decimal inrAmount);

    }
}
