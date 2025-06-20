using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VagueVault.Backend.Models.Carts;

namespace VagueVault.Backend.Configurations
{
    public class ConfigureCartRelation:IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> entity)
        {
            entity.HasOne(c => c.user).WithOne(c=>c.Cart).HasForeignKey<Cart>(c=>c.UserId);

            
        }
    }
}
