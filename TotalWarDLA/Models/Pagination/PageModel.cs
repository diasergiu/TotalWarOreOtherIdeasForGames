using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalWarDLA.Models.Pagination
{
    // not used yet. it seems like i can do it with just the information that are send from url
    public class PageModel<T> : List<T>
    {
        public int CurrentPage { get;  set; }
        public int TotalPages { get;  set; }
        public int PageSize { get; set; }
        public int TotalCount { get;  set; }

        public bool HasPrevios => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PageModel()
        {

        }
        public PageModel(List<T> items, int count, int pageNumber, int pageSize)
        {
            this.TotalCount = count;
            this.PageSize = pageSize;
            this.CurrentPage = pageNumber;
            this.TotalPages =(int)Math.Ceiling((double)count / pageSize);

            AddRange(items);
        }

        public static PageModel<T> ToPageModel(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PageModel<T>(items, source.Count(), pageNumber, pageSize);
        }

    }
}
