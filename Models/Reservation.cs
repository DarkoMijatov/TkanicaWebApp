using System.ComponentModel.DataAnnotations;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class Reservation : ITrackable
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool Active { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public List<ClothingReservation> ClothingReservations { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
