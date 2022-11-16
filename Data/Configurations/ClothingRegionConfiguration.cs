using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class ClothingRegionConfiguration : IEntityTypeConfiguration<ClothingRegion>
    {
        public void Configure(EntityTypeBuilder<ClothingRegion> builder)
        {
            
        }
    }
}
