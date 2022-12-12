using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasOne(x => x.MembershipFee)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.MembershipFeeId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User)
                .WithOne(x => x.Member)
                .HasForeignKey<Member>(x => x.UserId);
        }
    }
}
