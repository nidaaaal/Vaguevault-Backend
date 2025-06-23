using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Data;
using VagueVault.Backend.Middleware;
using VagueVault.Backend.Models.Product;

namespace VagueVault.Backend.Services.Statuses
{
    public class CategoryStatusServices:ICategoryStatusServices
    {
        private readonly VagueVaultDbContext _dbContext;
        public CategoryStatusServices(VagueVaultDbContext vagueVaultDbContext) 
        {
                    _dbContext = vagueVaultDbContext;
        }

        public async Task<IEnumerable<Categories>> ViewCategories()
        {
           var data = await _dbContext.Categories.ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Status>> ViewStatus()
        {
            var data = await _dbContext.Status.ToListAsync();
            return data;
        }

        public async Task<Categories> AddCategory(string name)
        {
            var cat = new Categories
            {
                Name = name
            };

            _dbContext.Categories.Add(cat);
            await _dbContext.SaveChangesAsync();
            return cat;
        }
        public async Task<Status> AddStatus(string name)
        {
            var stat = new Status
            {
                Name = name
            };

            _dbContext.Status.Add(stat);
            await _dbContext.SaveChangesAsync();
            return stat;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var data = await _dbContext.Categories.FirstOrDefaultAsync(s => s.Id == id);
            if (data == null) throw new BadRequestException("Invalid Id!");
            _dbContext.Categories.Remove(data);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStatus(int id)
        {
            var data = await _dbContext.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (data == null) throw new BadRequestException("Invalid Id!");
            _dbContext.Status.Remove(data);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
