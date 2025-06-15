using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Models.Products;

namespace VagueVault.Backend.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<Products> GetByCategoryId(int id);
        Task<Products> GetByCategoryName(string categoryName);
        Task<Products?> GetByProductId(int id);
        Task<ProductVariants?> GetVariantByProduct(int productId);

    }
}
