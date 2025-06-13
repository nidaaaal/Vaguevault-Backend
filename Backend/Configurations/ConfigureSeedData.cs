using Microsoft.EntityFrameworkCore;
using System.Drawing;
using VagueVault.Backend.Models.Products;

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
            new Status { Id = 4, Name = "OutOfStock" }
            );

            modelBuilder.Entity<Categories>().HasData(
            new Categories { Id = 1, Name = "Women" },
            new Categories { Id = 2, Name = "Men" },
            new Categories { Id = 3, Name = "Kids" },
            new Categories { Id = 4, Name = "Unisex" }
            );

            modelBuilder.Entity<Sizes>().HasData(
            new Sizes { Id = 1, Name = "XS" },
            new Sizes { Id = 2, Name = "S" },
            new Sizes { Id = 3, Name = "M" },
            new Sizes { Id = 4, Name = "L" },
            new Sizes { Id = 5, Name = "XL" },
            new Sizes { Id = 6, Name = "XXL" },
            new Sizes { Id = 7, Name = "One Size" }
            );

            modelBuilder.Entity<Colors>().HasData(
           new Colors { Id = 1, Name = "Cream" },
           new Colors { Id = 2, Name = "Black" },
           new Colors { Id = 3, Name = "White" },
           new Colors { Id = 4, Name = "Navy" },
           new Colors { Id = 5, Name = "Red" },
           new Colors { Id = 6, Name = "Green" }
       );

        }
    }
}
