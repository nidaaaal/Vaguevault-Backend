using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VagueVault.Backend.Configurations;
using VagueVault.Backend.Models.Addresses;
using VagueVault.Backend.Models.Auth;
using VagueVault.Backend.Models.Carts;
using VagueVault.Backend.Models.Product;
using VagueVault.Backend.Models.Wishlists;

namespace VagueVault.Backend.Data
{
    public class VagueVaultDbContext:DbContext
    {
        public VagueVaultDbContext(DbContextOptions<VagueVaultDbContext> options) : base(options) { }   

        public DbSet<Users> Users { get; set; }

        public DbSet<Products> Products { get; set; }   
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }   
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItems> CartItems { get; set; }

        public DbSet<Address> Address { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ConfigureProductRelation());
            modelBuilder.ApplyConfiguration(new ConfigureUsersRelation());
            modelBuilder.ApplyConfiguration(new ConfigureWishlistRelation());
            modelBuilder.ApplyConfiguration(new ConfigureCartRelation());
            modelBuilder.ApplyConfiguration(new ConfigureCartItemRelation()); 
            modelBuilder.ApplyConfiguration(new ConfigureAddressRelation());
            ConfigureSeedData.Seed(modelBuilder);

            
        }
    }
}
