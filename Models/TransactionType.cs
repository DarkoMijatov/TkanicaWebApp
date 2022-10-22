using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class TransactionType : ITrackable
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
        public short Direction { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
