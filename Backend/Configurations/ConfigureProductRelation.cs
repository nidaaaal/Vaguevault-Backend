using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using VagueVault.Backend.Models.Product;

namespace VagueVault.Backend.Configurations
{
    public class ConfigureProductRelation:IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> entity)
        {
                entity.HasOne(p => p.Status).WithMany(p => p.Products)
                .HasForeignKey(p => p.StatusId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Categories).WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");

                

                entity.HasIndex(p => p.Name);
                entity.HasIndex(p => p.StatusId);
                entity.HasIndex(p => p.CategoryId);
        }
    }
}
