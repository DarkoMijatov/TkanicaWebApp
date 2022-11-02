namespace TkanicaWebApp.ViewModels
{
    public class PageViewModel<T>
    {
        public string CurrentSort { get; set; }
        public string Search { get; set; }
        public List<T> List { get; set; }
        public int PageIndex { get; set; } = 1;
        public bool HasPreviousPage { get; set; } = false;
        public bool HasNextPage { get; set; } = true;
    }
}
