﻿namespace TaskManagementSystem.Models
{
    public class Sort
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }

        public string Category { get; set; } = string.Empty;
        public bool IsAsc { get; set; } = false;

    }
}
