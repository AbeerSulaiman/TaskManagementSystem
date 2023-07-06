namespace TaskManagementSystem.Models
{
    public class Filter
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }

        public int Priority { get; set; } = 0;
        public int? Status { get; set; } = null;
        public DateTime? DueDate { get; set; } = null;

    }
}
