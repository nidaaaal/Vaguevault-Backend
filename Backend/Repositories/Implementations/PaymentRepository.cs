using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VagueVault.Backend.Data;
using VagueVault.Backend.Repositories.Interface;

namespace VagueVault.Backend.Repositories.Implementations
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly VagueVaultDbContext _context;

        public PaymentRepository(VagueVaultDbContext vagueVaultDbContext)
        {
            _context = vagueVaultDbContext;
        }
       public async Task<bool> UpdateStock(int id,int count)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            product.Stock -= count;
            await _context.SaveChangesAsync();  
            return true;

        }


        public  decimal ConvertINRtoUSD(decimal inrAmount)
        {
            decimal conversionRate = 83.25m; // Example rate. You can update it manually or via API.
            return Math.Round(inrAmount / conversionRate, 2);
        }
    }
}
