using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Models.Products;
using VagueVault.Backend.Repositories.Interface;

namespace VagueVault.Backend.Repositories.Implementations
{
    public class ProductRepository: IProductRepository
    {
        private readonly VagueVaultDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductRepository(VagueVaultDbContext vagueVaultDbContext,IMapper mapper)
        {
            _dbContext = vagueVaultDbContext;
            _mapper = mapper;
        }

        public async Task<Products> GetByCategoryId(int id)
        {
          var result = await _dbContext.Products.Where(p=>p.CategoryId == id).ToListAsync();
            if (result == null) return null;

            return _mapper.Map<Products>(result);
        }
        public async Task<Products?> GetByCategoryName(string categoryName)
        {
            var result = await _dbContext.Products.Where(p => p.Categories.Name == categoryName).ToListAsync();
            if (result == null) return null;

            return _mapper.Map<Products>(result);
        }
        public async Task<Products?> GetByProductId(int id)
        {
            var data = await _dbContext.Products.Include(v => v.Variants).FirstOrDefaultAsync(x => x.Id == id);
            if (data == null) return null;
            return _mapper.Map<Products>(data);

        }
        public async Task<ProductVariants?> GetVariantByProduct(int productId)
        {
            var result = await _dbContext.ProductVariants.Where(p => p.Id == productId).ToListAsync();
            if (result == null) return null;

            return _mapper.Map<ProductVariants>(result);
        }



    }
}
