using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class PayPeriodConfiguration : IEntityTypeConfiguration<PayPeriod>
    {
        public void Configure(EntityTypeBuilder<PayPeriod> builder)
        {
            builder.HasMany(x => x.Employees)
                .WithOne(x => x.PayPeriod)
                .HasForeignKey(x => x.PayPeriodId)
                .IsRequired();
            builder.HasData(
                new PayPeriod { Id = 1, Name = "dnevno" },
                new PayPeriod { Id = 2, Name = "nedeljno" },
                new PayPeriod { Id = 3, Name = "mesečno" }
                );
        }
    }
}
