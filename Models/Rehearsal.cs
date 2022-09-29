using System.ComponentModel.DataAnnotations;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class Rehearsal : ITrackable
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int MemberGroupId { get; set; }
        public MemberGroup MemberGroup { get; set; }
        public List<RehearsalEmployee> RehearsalEmployees { get; set; }
        public List<RehearsalMember> RehearsalMembers { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
