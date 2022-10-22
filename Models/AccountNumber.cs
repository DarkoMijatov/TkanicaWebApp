using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class AccountNumber : ITrackable
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(30)")]
        public string BankAccountNumber { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(30)")]
        public string Bank { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public int? BalanceId { get; set; }
        public Balance Balance { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
