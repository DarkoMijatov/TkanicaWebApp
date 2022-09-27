using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class MemberGroupConfiguration : IEntityTypeConfiguration<MemberGroup>
    {
        public void Configure(EntityTypeBuilder<MemberGroup> builder)
        {
            builder.HasData(
                new MemberGroup { Id = 1, Name = "Prvi ansambl", Active = true },
                new MemberGroup { Id = 2, Name = "Dečji ansambl", Active = true },
                new MemberGroup { Id = 3, Name = "Rekreativna grupa", Active = false }
            );
            builder.HasMany(x => x.EmployeeMemberGroups)
                .WithOne(x => x.MemberGroup)
                .HasForeignKey(x => x.MemberGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
