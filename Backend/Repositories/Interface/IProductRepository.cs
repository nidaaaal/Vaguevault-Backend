using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Models.Products;

namespace VagueVault.Backend.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<ProductDto?> GetByCategoryId(int id);
        Task<ProductDto?> GetByCategoryName(string categoryName);
        Task<Products?> GetByProductId(int id);
        Task<ProductVariantDto?> GetVariantByProduct(int productId);

    }
}
