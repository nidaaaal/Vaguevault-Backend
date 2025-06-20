using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VagueVault.Backend.Models.Wishlists;

namespace VagueVault.Backend.Configurations
{
    public class ConfigureWishlistRelation:IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> entity)
        {
            entity.HasOne(w=>w.users).WithMany(w=>w.wishlists).HasForeignKey(w=>w.UserId);

            entity.HasOne(w => w.Products).WithMany(w => w.wishlists).HasForeignKey(w => w.ProductId);

            entity.HasIndex(w=> new {w.UserId,w.ProductId}).IsUnique();
        }
    }
}
