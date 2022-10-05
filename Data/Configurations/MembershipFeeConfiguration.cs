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
        }
    }
}
