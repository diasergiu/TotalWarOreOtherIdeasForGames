using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models.Pagination;

namespace TotalWarOreOtherIdeasForGames.ViewModel
{
    public class IndexViewModel<T>
    {
        IEnumerable<T> ListModel { get; set; }
        PageInformationSender page { get; set; }
    }
}
