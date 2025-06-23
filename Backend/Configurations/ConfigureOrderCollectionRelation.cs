using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VagueVault.Backend.Models.Order;

namespace VagueVault.Backend.Configurations
{
    public class ConfigureOrderCollectionRelation:IEntityTypeConfiguration<OrderCollections>
    {
        public void Configure(EntityTypeBuilder<OrderCollections> entity)
        {
            entity.HasOne(e=>e.Orders).WithMany(e=>e.OrderCollections).HasForeignKey(e=>e.OrderId).OnDelete(DeleteBehavior.Cascade);    
            entity.HasOne(e=>e.Products).WithMany(e=>e.OrderCollections).HasForeignKey(e=>e.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
