using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Models.Products;

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
        Task<Categories> AddCategory(int id, string name);
        Task<Status> AddStatus(int id, string name);

        Task<bool> DeleteCategory(int id);

        Task<bool> DeleteStatus(int id);




    }
}
