using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations
{
    public class HorsesOperations
    {
        public TotalWarWanaBeContext _context;

        public HorsesOperations(TotalWarWanaBeContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Horse>> GetPageOfHorses(PageInformationSender page)
        {
            return await _context.Horses.Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize).ToListAsync();
        }
    }
}
