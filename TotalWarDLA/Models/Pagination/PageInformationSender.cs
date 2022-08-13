using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalWarDLA.Models.Pagination
{
    // i tray to implement it without a maxPageSizeProperty
    public class PageInformationSender
    {
        public int TotalPages { get; set; }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        public PageInformationSender(int numberOfItems, int pageNumber, int pageSize)
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.TotalPages = (int)Math.Ceiling(numberOfItems / (double)PageSize);
        }

    }
}
