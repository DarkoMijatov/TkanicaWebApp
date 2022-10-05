using System.ComponentModel.DataAnnotations;

namespace TkanicaWebApp.ViewModels
{
    public class RehearsalViewModel
    {
        public int? Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public List<int> MemberGroupIds { get; set; }
        public List<int> EmployeeIds { get; set; }
        public List<int> MemberIds { get; set; }
    }
}
