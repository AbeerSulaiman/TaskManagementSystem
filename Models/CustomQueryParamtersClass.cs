namespace TaskManagementSystem.Models
{
    public class CustomQueryParameters
    {
        private const int maxPageCount = 1000;
        public int Page { get; set; } = 1;
        public string Search { get; set; } = null;
        private int _pageCount = 40;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value > maxPageCount ? maxPageCount : value; }
        }
    }
}
