using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class ClothingReservationConfiguration : IEntityTypeConfiguration<ClothingReservation>
    {
        public void Configure(EntityTypeBuilder<ClothingReservation> builder)
        {
            builder.HasOne(x => x.Clothing)
                .WithMany(x => x.ClothingReservations)
                .HasForeignKey(x => x.ClothingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Reservation)
                .WithMany(x => x.ClothingReservations)
                .HasForeignKey(x => x.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
