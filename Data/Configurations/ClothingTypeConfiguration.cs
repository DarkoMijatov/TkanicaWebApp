using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class ClothingTypeConfiguration : IEntityTypeConfiguration<ClothingType>
    {
        public void Configure(EntityTypeBuilder<ClothingType> builder)
        {
            
        }
    }
}
