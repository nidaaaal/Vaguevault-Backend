using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VagueVault.Backend.Models.Addresses;

namespace VagueVault.Backend.Configurations
{
    public class ConfigureAddressRelation:IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.HasOne(p=>p.Users).WithMany(p=>p.Address).HasForeignKey(p=>p.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
