using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VagueVault.Backend.Configurations;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Products;

namespace VagueVault.Backend.Data
{
    public class VagueVaultDbContext:DbContext
    {
        public VagueVaultDbContext(DbContextOptions<VagueVaultDbContext> options) : base(options) { }   

        public DbSet<Users> Users { get; set; }

        public DbSet<Products> Products { get; set; }   
        public DbSet<ProductVariants> ProductVariants { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Colors> Colors { get; set; }   
        public DbSet<Sizes> Sizes { get; set; } 
        public DbSet<Status> Status { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ConfigureProductRelation());
            modelBuilder.ApplyConfiguration(new ConfigureProductVariant());
            ConfigureSeedData.Seed(modelBuilder);

            
        }
    }
}
