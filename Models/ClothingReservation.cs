using System.ComponentModel.DataAnnotations;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class ClothingReservation : ITrackable
    {
        public int Id { get; set; }
        public int ClothingId { get; set; }
        public Clothing Clothing { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
