using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.EarningAmount)
                .HasPrecision(14, 2);
            builder.Property(x => x.OtherExpenses)
                .HasPrecision(14, 2);
            builder.HasMany(x => x.EmployeeMemberGroups)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User)
                .WithOne(x => x.Employee)
                .HasForeignKey<Employee>(x => x.UserId);
        }
    }
}
