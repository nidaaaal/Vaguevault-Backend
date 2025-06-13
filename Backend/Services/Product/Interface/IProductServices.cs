using VagueVault.Backend.DTOs.Products;

namespace VagueVault.Backend.Services.Product.Interface
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto?> CreateProductAsync(ProductDto product);
        Task<ProductDto?> UpdateProductAsync(int id, ProductDto product);
        Task<bool> DeleteProductAsync(int id);
    }
}
