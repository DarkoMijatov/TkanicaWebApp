using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class ClothingRegionConfiguration : IEntityTypeConfiguration<ClothingRegion>
    {
        public void Configure(EntityTypeBuilder<ClothingRegion> builder)
        {
            builder.HasData(
                new ClothingRegion { Id = 1, Name = "Banat", AreaId = 1 },
                new ClothingRegion { Id = 2, Name = "Šumadija", AreaId = 2 },
                new ClothingRegion { Id = 3, Name = "Vranjsko polje", AreaId = 5 },
                new ClothingRegion { Id = 4, Name = "Pčinja", AreaId = 5 }
                );
        }
    }
}
