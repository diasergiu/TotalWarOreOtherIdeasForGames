using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;

namespace TotalWarOreOtherIdeasForGames.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Item> ListModel { get; set; }
        public PageModel<Item> Page { get; set; }
        public Item Items { get; set; }
    }
}
