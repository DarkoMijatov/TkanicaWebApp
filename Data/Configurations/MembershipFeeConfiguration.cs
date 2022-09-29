using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class MembershipFeeConfiguration : IEntityTypeConfiguration<MembershipFee>
    {
        public void Configure(EntityTypeBuilder<MembershipFee> builder)
        {
            builder.HasOne(x => x.MemberGroup)
                .WithMany(x => x.MembershipFees)
                .HasForeignKey(x => x.MemberGroupId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Amount)
                .HasPrecision(14, 2);

            builder.HasData(
                new MembershipFee { Id = 1, Name = "Prvi ansambl", MemberGroupId = 1, Amount = 1000 },
                new MembershipFee { Id = 2, Name = "Prvi ansambl popust", MemberGroupId = 1, Amount = 500 },
                new MembershipFee { Id = 3, Name = "Dečji ansambl", MemberGroupId = 2, Amount = 800 },
                new MembershipFee { Id = 4, Name = "Dečji ansambl popust", MemberGroupId = 2, Amount = 400 }
            );
        }
    }
}
