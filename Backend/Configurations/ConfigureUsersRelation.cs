using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VagueVault.Backend.Models.Auth;

namespace VagueVault.Backend.Configurations
{
    public class ConfigureUsersRelation:IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> entity) 
        {
            entity.HasOne(e =>e.Status).WithMany(e =>e.Users)
                .HasForeignKey(e => e.StatusId).OnDelete(DeleteBehavior.Restrict);



            entity.HasIndex(p => p.StatusId);

        }


    }
}
