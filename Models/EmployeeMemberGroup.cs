using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class EmployeeMemberGroup : ITrackable
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int MemberGroupId { get; set; }
        public MemberGroup MemberGroup { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
