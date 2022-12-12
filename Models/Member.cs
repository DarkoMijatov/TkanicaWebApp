using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class Member : ITrackable
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(20)")]
        public string FirstName { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(30)")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfEntry { get; set; }
        public bool Active { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Column(TypeName = "varchar(10)")]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(50)")]
        public string City { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(100)")]
        public string School { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(20)")]
        public string Class { get; set; }
        public int? YearsOfExperience { get; set; }
        [DataType(DataType.Url)]
        [Column(TypeName = "varchar(150)")]
        public string FacebookProfileUrl { get; set; }
        [DataType(DataType.Url)]
        [Column(TypeName = "varchar(150)")]
        public string InstagramProfileUrl { get; set; }
        [DataType(DataType.Url)]
        [Column(TypeName = "varchar(150)")]
        public string TikTokProfileUrl { get; set; }
        public byte[] ProfilePicture { get; set; }
        public int MembershipFeeId { get; set; }
        public MembershipFee MembershipFee { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
        public List<RehearsalMember> RehearsalMembers { get; set; }
        public List<Transaction> Transactions { get; set; }
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }
        public List<Reservation> Reservations { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
