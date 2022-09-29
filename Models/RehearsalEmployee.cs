using System.ComponentModel.DataAnnotations;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class RehearsalEmployee : ITrackable
    {
        public int Id { get; set; }
        public int RehearsalId { get; set; }
        public Rehearsal Rehearsal { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
