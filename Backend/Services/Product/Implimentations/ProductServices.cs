using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Models.Products;
using VagueVault.Backend.Repositories.Interface;
using VagueVault.Backend.Services.Product.Interface;

namespace VagueVault.Backend.Services.Product.Implimentations
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
          return  _mapper.Map<IEnumerable<ProductDto>>(products);
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
            data.CategoryId = product.Categories.Id;
            data.StatusId=product.Status.Id;
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




    }
}
