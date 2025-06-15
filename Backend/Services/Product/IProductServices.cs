using VagueVault.Backend.DTOs.Products;

namespace VagueVault.Backend.Services.Product
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto?> CreateProductAsync(ProductDto product);
        Task<ProductDto?> UpdateProductAsync(int id, ProductDto product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>?> GetProductBySearch(string search);
        Task<IEnumerable<ProductDto>?> GetProductByCategoriesId(int id);
        Task<ProductVariantDto?> CreateProductVariant(int id,ProductVariantDto productVariant);

    }
}
