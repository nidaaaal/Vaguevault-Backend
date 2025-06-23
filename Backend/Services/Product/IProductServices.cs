using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Models.Product;

namespace VagueVault.Backend.Services.Product
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto?> CreateProductAsync(ProductAddDto product);
        Task<ProductDto?> UpdateProductAsync(int id, ProductAddDto product);
        Task<ProductDto?> UpdateProductPriceAsync(int id, Decimal price);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>?> GetProductBySearch(string search);
        Task<IEnumerable<ProductDto>?> GetProductByCategoriesId(int id);

    }
}
