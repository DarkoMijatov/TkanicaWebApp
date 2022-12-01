using System.ComponentModel.DataAnnotations;

namespace TkanicaWebApp.ViewModels
{
    public class ReservationViewModel
    {
        public int? Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool Active { get; set; }
        public int MemberId { get; set; }
        public List<int> ClothingIds { get; set; }
    }
}
