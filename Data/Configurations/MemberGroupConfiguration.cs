using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class MemberGroupConfiguration : IEntityTypeConfiguration<MemberGroup>
    {
        public void Configure(EntityTypeBuilder<MemberGroup> builder)
        {
            builder.HasMany(x => x.EmployeeMemberGroups)
                .WithOne(x => x.MemberGroup)
                .HasForeignKey(x => x.MemberGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
