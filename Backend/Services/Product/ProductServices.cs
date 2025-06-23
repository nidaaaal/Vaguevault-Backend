using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Middleware;
using VagueVault.Backend.Models.Product;
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
          
                var products = await _dbContext.Products
                .Include(x=>x.Status)
                .Include(x=>x.Categories)
                .ToListAsync();
                return _mapper.Map<IEnumerable<ProductDto>>(products);
           
        }
        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var data = await _repository.GetByProductId(id);
            if (data == null) throw new NotFoundException("Invalid product Id");
             return _mapper.Map<ProductDto>(data);  
        }
       
           public async Task<ProductDto?> CreateProductAsync(ProductAddDto productDto){
            if (await _dbContext.Products.AnyAsync(x => x.Name == productDto.Name))
                throw new BadRequestException("Product Name Already Exist");
            productDto.Id = 0;

            var product = _mapper.Map<Products>(productDto);
            
            Console.WriteLine(product);
            var url = await _cloudinaryService.UploadImage(productDto.ImageFile, $"products/{productDto.Id}");
            
            product.ImageUrl = url;

            await _dbContext.Products.AddAsync(product);
            
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        

        public async Task<ProductDto?> UpdateProductAsync(int id, ProductAddDto productDto)
        {
            var data = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null) throw new NotFoundException("Invalid Product Id");
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

        public async Task<ProductDto?> UpdateProductPriceAsync(int id, Decimal price)
        {
            var data = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null) throw new NotFoundException("Invalid Product Id");
            data.Price = price;
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ProductDto>(data);

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var data = await _repository.GetByProductId(id);
            if (data == null) throw new NotFoundException("Invalid Product Id");

            data.IsDeleted = true; //inactive soft delete !

            await _dbContext.SaveChangesAsync();
            return true;
        }
       
        public async Task<IEnumerable<ProductDto>?> GetProductBySearch(string search)
        {
           var result = await _dbContext.Products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToListAsync();
            if (result == null) throw new NotFoundException("No products!");
            return _mapper.Map<IEnumerable<ProductDto>>(result);
        }


        public async Task<IEnumerable<ProductDto>?> GetProductByCategoriesId(int id)
        {
            var data = await _repository.GetByCategoryId(id);
            if (data == null) throw new NotFoundException("Invalid Category Id");
            return _mapper.Map<IEnumerable<ProductDto>>(data);
            
        }
        
}
}
