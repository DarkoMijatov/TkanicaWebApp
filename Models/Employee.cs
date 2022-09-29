using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class Employee : ITrackable
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(20)")]
        public string FirstName { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(30)")]
        public string LastName { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public int EarningTypeId { get; set; }
        public EarningType EarningType { get; set; }
        public decimal EarningAmount { get; set; }
        public int PayPeriodId { get; set; }
        public PayPeriod PayPeriod {get; set; }
        public decimal? OtherExpenses { get; set; }
        [DataType(DataType.Text)]
        [Column(TypeName = "varchar(50)")]
        public string? OtherExpensesDescription { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
        public List<EmployeeMemberGroup> EmployeeMemberGroups { get; set; }
        public List<RehearsalEmployee> RehearsalEmployees { get; set; }
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }
    }
}
