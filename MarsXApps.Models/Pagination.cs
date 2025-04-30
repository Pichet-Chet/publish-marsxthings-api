using System;
namespace MarsXApps.Models
{
    public class Pagination : Sorting
    {
        const int maxPageSize = 999;

        public bool isAll { get; set; } = false;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}

