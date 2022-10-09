namespace TkanicaWebApp.ViewModels
{
    public class ClientViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsCompany { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string IdNumber { get; set; }
        public string TaxNumber { get; set; }
        public byte[] Logo { get; set; }
        public List<int> AccountNumberIds { get; set; } 
    }
}
