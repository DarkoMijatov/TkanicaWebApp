using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(x => x.Member)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.MemberId);
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.EmployeeId);
            builder.HasOne(x => x.TransactionType)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.TransactionTypeId)
                .IsRequired();
            builder.HasOne(x => x.Creditor)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.CreditorId);
            builder.HasOne(x => x.Debtor)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.DebtorId);
            builder.HasOne(x => x.Balance)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.BalanceId)
                .IsRequired();
        }
    }
}
