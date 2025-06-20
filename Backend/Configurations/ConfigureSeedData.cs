using Microsoft.EntityFrameworkCore;
using System.Drawing;
using VagueVault.Backend.Models.Product;

namespace VagueVault.Backend.Configurations
{
    public class ConfigureSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "Active" },
            new Status { Id = 2, Name = "Inactive" },
            new Status { Id = 3, Name = "Discontinued" },
            new Status { Id = 4, Name = "OutOfStock" },
            new Status { Id = 5, Name = "Suspended"}
            );

            modelBuilder.Entity<Categories>().HasData(
            new Categories { Id = 1, Name = "Women" },
            new Categories { Id = 2, Name = "Men" },
            new Categories { Id = 3, Name = "Kids" },
            new Categories { Id = 4, Name = "Unisex" }
            );

        }
    }
}
