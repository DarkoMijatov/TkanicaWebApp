using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class MembershipFee : ITrackable
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(30)")]
        [DataType(DataType.Text)]
        public string Name { get; set; } 
        public int MemberGroupId { get; set; }
        public MemberGroup MemberGroup { get; set; }
        [Column(TypeName = "decimal(14, 2)")]
        public decimal Amount { get; set; }
        public List<Member> Members { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
