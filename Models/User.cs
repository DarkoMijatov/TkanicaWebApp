using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class User : ITrackable
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "varchar(30)")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public bool Verified { get; set; }
        public byte[] ProfilePicture { get; set; }
        public int? MemberId { get; set; }
        public Member Member { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
