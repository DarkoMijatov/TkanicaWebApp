namespace TkanicaWebApp.ViewModels
{
    public class PageViewModel<T>
    {
        public string CurrentSort { get; set; }
        public List<T> List { get; set; }
    }
}
