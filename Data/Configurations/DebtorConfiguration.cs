using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class DebtorConfiguration : IEntityTypeConfiguration<Debtor>
    {
        public void Configure(EntityTypeBuilder<Debtor> builder)
        {
            builder.HasMany(x => x.AccountNumbers)
                .WithOne(x => x.Debtor)
                .HasForeignKey(x => x.DebtorId);
            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Debtor)
                .HasForeignKey(x => x.DebtorId);
        }
    }
}
