using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class MembershipFeeDebtUpdate : ITrackable
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
