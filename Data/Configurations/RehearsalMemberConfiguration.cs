using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class RehearsalMemberConfiguration : IEntityTypeConfiguration<RehearsalMember>
    {
        public void Configure(EntityTypeBuilder<RehearsalMember> builder)
        {
            builder.HasOne(x => x.Member)
                .WithMany(x => x.RehearsalMembers)
                .HasForeignKey(x => x.MemberId)
                .IsRequired();
            builder.HasOne(x => x.Rehearsal)
                .WithMany(x => x.RehearsalMembers)
                .HasForeignKey(x => x.RehearsalId)
                .IsRequired();
        }
    }
}
