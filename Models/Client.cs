using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class Client : ITrackable
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
        [DataType(DataType.Url)]
        [Column(TypeName = "varchar(100)")]
        public string Website { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(10)")]
        public string IdNumber { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(10)")]
        public string TaxNumber { get; set; }
        public byte[] Logo { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
        public List<Transaction> CreditorTransactions { get; set; }
        public List<Transaction> DebtorTransactions { get; set; }
        public List<AccountNumber> AccountNumbers { get; set; }
    }
}
