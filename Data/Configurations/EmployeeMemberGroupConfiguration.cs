using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class EmployeeMemberGroupConfiguration : IEntityTypeConfiguration<EmployeeMemberGroup>
    {
        public void Configure(EntityTypeBuilder<EmployeeMemberGroup> builder)
        {
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.EmployeeMemberGroups)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.MemberGroup)
                .WithMany(x => x.EmployeeMemberGroups)
                .HasForeignKey(x => x.MemberGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
