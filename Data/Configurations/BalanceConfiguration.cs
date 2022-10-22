using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Balance)
                .HasForeignKey(x => x.BalanceId)
                .IsRequired();
            builder.HasOne(x => x.AccountNumber)
                .WithOne(x => x.Balance)
                .HasForeignKey<Balance>(x => x.AccountNumberId);
            builder.HasData(
                new Balance { Id = 1, Name = "kasa", IsCash = true, AccountNumberId = null },
                new Balance { Id = 2, Name = "račun banka", IsCash = false, AccountNumberId = 1 },
                new Balance { Id = 3, Name = "račun trezor", IsCash = false, AccountNumberId = 2 }
                );
        }
    }
}
