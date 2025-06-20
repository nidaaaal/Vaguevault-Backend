using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Models.Product;

namespace VagueVault.Backend.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>?> GetByCategoryId(int id);
        Task<Products?> GetByProductId(int id);

    }
}
