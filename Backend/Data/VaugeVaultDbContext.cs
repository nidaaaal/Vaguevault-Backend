using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Models.Auth;

namespace VagueVault.Backend.Data
{
    public class VagueVaultDbContext:DbContext
    {
        public VagueVaultDbContext(DbContextOptions<VagueVaultDbContext> options) : base(options) { }   

        public DbSet<Users> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
    }
}
