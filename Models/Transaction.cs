using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class Transaction : ITrackable
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(20)")]
        public string TransactionNumber { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int? DebtorId { get; set; }
        public Debtor Debtor { get; set; }
        public int? CreditorId { get; set; }
        public Creditor Creditor { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(30)")]
        public string ReferenceNumber { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(14,2)")]
        public decimal Amount { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; }
        public bool Paid { get; set; }
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PaidDate { get; set; }
        public int? MemberId { get; set; }
        public Member Member { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int BalanceId { get; set; }
        public Balance Balance { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
