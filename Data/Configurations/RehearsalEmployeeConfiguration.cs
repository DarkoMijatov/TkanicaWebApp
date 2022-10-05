using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class RehearsalEmployeeConfiguration : IEntityTypeConfiguration<RehearsalEmployee>
    {
        public void Configure(EntityTypeBuilder<RehearsalEmployee> builder)
        {
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.RehearsalEmployees)
                .HasForeignKey(x => x.EmployeeId)
                .IsRequired();
            builder.HasOne(x => x.Rehearsal)
                .WithMany(x => x.RehearsalEmployees)
                .HasForeignKey(x => x.RehearsalId)
                .IsRequired();
        }
    }
}
