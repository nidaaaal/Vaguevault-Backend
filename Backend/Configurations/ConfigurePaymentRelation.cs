using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VagueVault.Backend.Models.Order;

namespace VagueVault.Backend.Configurations
{
    public class ConfigurePaymentRelation : IEntityTypeConfiguration<PaymentMethods>
    {
        public void Configure(EntityTypeBuilder<PaymentMethods> entity)
        {
            entity.HasMany(e=>e.Orders).WithOne(e=>e.PaymentMethods).HasForeignKey(e=>e.PaymentMethodId);
           
            
        }

            

    }
}
