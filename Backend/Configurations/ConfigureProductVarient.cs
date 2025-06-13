using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Models.Products;

namespace VagueVault.Backend.Configurations
{
    public class ConfigureProductVariant:IEntityTypeConfiguration<ProductVariants>
    {
        public void Configure(EntityTypeBuilder<ProductVariants> entity)
        {
            
                entity.HasOne(v => v.Colors).WithMany(v => v.Variants)
                .HasForeignKey(v => v.ColorId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(v => v.Sizes).WithMany(v => v.Variants)
                .HasForeignKey(v => v.SizeId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

            entity.HasIndex(v => v.ProductId);
            entity.HasIndex(v => new {v.SizeId,v.ColorId});

        }
    }
}
