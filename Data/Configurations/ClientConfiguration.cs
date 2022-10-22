using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasMany(x => x.AccountNumbers)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId);
            builder.HasMany(x => x.CreditorTransactions)
                .WithOne(x => x.Creditor)
                .HasForeignKey(x => x.CreditorId);
            builder.HasMany(x => x.DebtorTransactions)
                .WithOne(x => x.Debtor)
                .HasForeignKey(x => x.DebtorId);
        }
    }
}
