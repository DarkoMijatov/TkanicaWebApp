using TkanicaWebApp.Models;

namespace TkanicaWebApp.Classes
{
    public class PresenceTracking
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public decimal JanuaryPercentage { get; set; }
        public decimal FebruaryPercentage { get; set; }
        public decimal MarchPercentage { get; set; }
        public decimal AprilPercentage { get; set; }
        public decimal MayPercentage { get; set; }
        public decimal JunePercentage { get; set; }
        public decimal JulyPercentage { get; set; }
        public decimal AugustPercentage { get; set; }
        public decimal? SeptemberPercentage { get; set; }
        public decimal? OctoberPercentage { get; set; }
        public decimal? NovemberPercentage { get; set; }
        public decimal? DecemberPercentage { get; set; }
        public decimal? TotalPercentage { get; set; }
    }
}
