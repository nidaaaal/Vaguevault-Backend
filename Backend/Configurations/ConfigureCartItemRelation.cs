using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VagueVault.Backend.Models.Carts;

namespace VagueVault.Backend.Configurations
{
    public class ConfigureCartItemRelation:IEntityTypeConfiguration<CartItems>
    {
        public void Configure(EntityTypeBuilder<CartItems> entity) 
        {
            entity.HasOne(c => c.Product).WithMany().HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.Restrict);    
            entity.HasOne(c=>c.Carts).WithMany(c=>c.Items).HasForeignKey(c => c.CartId).OnDelete(DeleteBehavior.Cascade);
            entity.Property(e=>e.Quantity).IsRequired();    
        }
    }
}
