using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class PayPeriod : ITrackable
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
