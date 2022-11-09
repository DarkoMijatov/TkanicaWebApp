using TkanicaWebApp.Models;

namespace TkanicaWebApp.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfEntry { get; set; }
        public string Active { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string School { get; set; }
        public string Class { get; set; }
        public int? YearsOfExperience { get; set; }
        public string FacebookProfileUrl { get; set; }
        public string InstagramProfileUrl { get; set; }
        public string TikTokProfileUrl { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string MemberGroupName { get; set; }
        public decimal MembershipFeeAmount { get; set; }
        public decimal DebtAmount { get; set; }
        public int RehearsalsCount { get; set; }
        public string PresenceTrackingPercentage { get; set; }
        public List<Rehearsal> Rehearsals { get; set; }
        public List<Transaction> Transactions { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
