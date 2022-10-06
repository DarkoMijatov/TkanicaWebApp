using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class Debtor : ITrackable
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        public bool IsCompany { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(50)")]
        public string Address { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(30)")]
        public string City { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Column(TypeName = "varchar(10)")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "varchar(30)")]
        public string Email { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(10)")]
        public string IdNumber { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(10)")]
        public string TaxNumber { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<AccountNumber> AccountNumbers { get; set; }
    }
}
