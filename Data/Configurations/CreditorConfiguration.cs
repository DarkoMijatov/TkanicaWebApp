using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class CreditorConfiguration : IEntityTypeConfiguration<Creditor>
    {
        public void Configure(EntityTypeBuilder<Creditor> builder)
        {
            builder.HasMany(x => x.AccountNumbers)
                .WithOne(x => x.Creditor)
                .HasForeignKey(x => x.CreditorId);
            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Creditor)
                .HasForeignKey(x => x.CreditorId);
        }
    }
}
