using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VagueVault.Backend.Models.Order;

namespace VagueVault.Backend.Configurations
{
    public class ConfigureOrdersRelation : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> entity)
        {
            entity.HasOne(e=>e.Users).WithMany(e=>e.Orders).HasForeignKey(e=>e.UserId).OnDelete(DeleteBehavior.Restrict);   
            entity.HasOne(e=>e.Address).WithMany(e=>e.Orders).HasForeignKey(e=>e.ShippingAddressId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Status).WithMany(e => e.Orders).HasForeignKey(e => e.StatusId).OnDelete(DeleteBehavior.Restrict);

           
        }
    }
}
