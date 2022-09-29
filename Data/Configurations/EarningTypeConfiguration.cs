using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class EarningTypeConfiguration : IEntityTypeConfiguration<EarningType>
    {
        public void Configure(EntityTypeBuilder<EarningType> builder)
        {
            builder.HasMany(x => x.Employees)
                .WithOne(x => x.EarningType)
                .HasForeignKey(x => x.EarningTypeId)
                .IsRequired();
            builder.HasData(
                new EarningType { Id = 1, Name = "fiksno" },
                new EarningType { Id = 2, Name = "po probi" },
                new EarningType { Id = 3, Name = "po članu" }
                );
        }
    }
}
