using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class AccountNumberConfiguration : IEntityTypeConfiguration<AccountNumber>
    {
        public void Configure(EntityTypeBuilder<AccountNumber> builder)
        {
            builder.HasOne(x => x.Creditor)
                .WithMany(x => x.AccountNumbers)
                .HasForeignKey(x => x.CreditorId);
            builder.HasOne(x => x.Debtor)
                .WithMany(x => x.AccountNumbers)
                .HasForeignKey(x => x.DebtorId);
            builder.HasOne(x => x.Balance)
                .WithOne(x => x.AccountNumber)
                .HasForeignKey<AccountNumber>(x => x.BalanceId);
            builder.HasData(
                new AccountNumber { Id = 1, BankAccountNumber = "200-3169580101844-71", Bank = "Poštanska štedionica A.D.", BalanceId = 2 },
                new AccountNumber { Id = 2, BankAccountNumber = "840-49151763-68", Bank = "Uprava za trezor", BalanceId = 3 }
                );
        }
    }
}
