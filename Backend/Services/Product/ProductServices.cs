using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Models.Products;
using VagueVault.Backend.Repositories.Interface;

namespace VagueVault.Backend.Services.Product
{
    public class ProductServices: IProductServices
    {
        private readonly VagueVaultDbContext _dbContext;
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductServices(VagueVaultDbContext dbContext,IMapper mapper,IProductRepository repository) 
        { 
            _dbContext = dbContext;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
          
                var products = await _dbContext.Products.ToListAsync();
                return _mapper.Map<IEnumerable<ProductDto>>(products);
           
        }
        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var data = await _repository.GetByProductId(id);
             return _mapper.Map<ProductDto>(data);  
        }
        public async Task<ProductDto?> CreateProductAsync(ProductDto product)
        {
            if (await _dbContext.Products.AnyAsync(x => x.Name == product.Name || x.Id==product.Id)) return null;
          var data = _mapper.Map<Products>(product);
            _dbContext.Products.Add(data);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ProductDto>(data); 


        }
        public async Task<ProductDto?> UpdateProductAsync(int id, ProductDto product)
        {
            var data =await _dbContext.Products.Include(v=>v.Variants).FirstOrDefaultAsync(x => x.Id == id);
            if (data == null) return null;

            data.Id = product.Id;
            data.Name=product.Name;
            data.Price=product.Price;
            data.Description=product.Description;
            data.CategoryId = product.CategoryId;
            data.StatusId=product.StatusId;
            data.UpdatedAt=DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(data);  
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var data = await _repository.GetByProductId(id);
            if(data== null) return false;   

            data.StatusId = 2; //inactive soft delete !

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>?> GetProductBySearch(string search)
        {
           var result = await _dbContext.Products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToListAsync();
            if (result == null) return null;
            return _mapper.Map<IEnumerable<ProductDto>>(result);
        }

        public async Task<IEnumerable<ProductDto>?> GetProductByCategoriesId(int id)
        {
            var data = await _repository.GetByCategoryId(id);
            if (data== null) return null;   
            return _mapper.Map<IEnumerable<ProductDto>>(data);
            
        }

        public async Task<ProductVariantDto?> CreateProductVariant(int id, ProductVariantDto productVariant)
        {
            var data = await _repository.GetByProductId(id);
            if (data== null) return null;
            var product =  _mapper.Map<ProductVariants>(productVariant);
            data.Variants.Add(product);
            await _dbContext.SaveChangesAsync();
            return productVariant;
        }






    }
}
