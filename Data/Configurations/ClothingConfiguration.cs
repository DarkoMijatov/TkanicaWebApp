using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class ClothingConfiguration : IEntityTypeConfiguration<Clothing>
    {
        public void Configure(EntityTypeBuilder<Clothing> builder)
        {
            builder.HasOne(x => x.ClothingRegion)
                .WithMany(x => x.Clothings)
                .HasForeignKey(x => x.ClothingRegionId);

            builder.HasOne(x => x.ClothingType)
                .WithMany(x => x.Clothings)
                .HasForeignKey(x => x.ClothingTypeId);
        }
    }
}
