using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.TransactionType)
                .HasForeignKey(x => x.TransactionTypeId)
                .IsRequired();

            builder.HasData(
                new TransactionType { Id = 1, Name = "članarina", Direction = 1 },
                new TransactionType { Id = 2, Name = "zarada", Direction = -1 },
                new TransactionType { Id = 3, Name = "donacija", Direction = 1 },
                new TransactionType { Id = 4, Name = "uplata gotovine", Direction = 1 },
                new TransactionType { Id = 5, Name = "isplata gotovine", Direction = -1 },
                new TransactionType { Id = 6, Name = "priliv na račun", Direction = 1 },
                new TransactionType { Id = 7, Name = "odliv sa računa", Direction = -1 }
                );
        }
    }
}
