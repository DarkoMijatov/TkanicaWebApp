using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class RehearsalConfiguration : IEntityTypeConfiguration<Rehearsal>
    {
        public void Configure(EntityTypeBuilder<Rehearsal> builder)
        {
            builder.HasMany(x => x.RehearsalMembers)
                .WithOne(x => x.Rehearsal)
                .HasForeignKey(x => x.RehearsalId)
                .IsRequired();
            builder.HasMany(x => x.RehearsalEmployees)
                .WithOne(x => x.Rehearsal)
                .HasForeignKey(x => x.RehearsalId)
                .IsRequired();
        }
    }
}
