using AutoMapper;
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
        private readonly ICloudinaryService _cloudinaryService;

        public ProductServices(VagueVaultDbContext dbContext,IMapper mapper,IProductRepository repository,ICloudinaryService cloudinaryService) 
        { 
            _dbContext = dbContext;
            _repository = repository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
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
       
           public async Task<ProductDto?> CreateProductAsync(ProductDto productDto){
            if (await _dbContext.Products.AnyAsync(x => x.Name == productDto.Name))
                return null;
            productDto.Id = 0;

            var product = _mapper.Map<Products>(productDto);
            
            Console.WriteLine(product);
            var url = await _cloudinaryService.UploadImage(productDto.ImageFile, $"products/{productDto.Id}");
            Console.WriteLine(url);

            product.ImageUrl = url;

            await _dbContext.Products.AddAsync(product);
            
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        

        public async Task<ProductDto?> UpdateProductAsync(int id, ProductDto productDto)
        {
            var data = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null) return null;
            await _cloudinaryService.DeleteImageAsync(data.ImageUrl);
            var url = await _cloudinaryService.UploadImage(productDto.ImageFile, $"products/{id}");

            data.Name= productDto.Name;
            data.Price= productDto.Price;
            data.Description= productDto.Description;
            data.CategoryId = productDto.CategoryId;
            data.StatusId= productDto.StatusId;
            data.UpdatedAt=DateTime.Now;
            data.ImageUrl = url;

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
        public async Task<Categories> AddCategory(int id,string name)
        {
            var cat = new Categories
            {
                Id = id,
                Name = name
            };

            _dbContext.Categories.Add(cat);
            await _dbContext.SaveChangesAsync();
            return cat;
        }
        public async Task<Status> AddStatus(int id, string name)
        {
            var stat = new Status
            {
                Id = id,
                Name = name
            };

            _dbContext.Status.Add(stat);
            await _dbContext.SaveChangesAsync();
            return stat;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var data = await _dbContext.Categories.FirstOrDefaultAsync(s => s.Id == id);
            if (data == null) return false;
            _dbContext.Categories.Remove(data);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStatus(int id)
         {
            var data = await _dbContext.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (data == null) return false;
            _dbContext.Status.Remove(data);
            await _dbContext.SaveChangesAsync();
            return true;
        }
}
}
