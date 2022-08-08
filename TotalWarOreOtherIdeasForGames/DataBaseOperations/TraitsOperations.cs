using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations
{
    public class TraitsOperations
    {
        public TotalWarWanaBeContext _context;

        public TraitsOperations(TotalWarWanaBeContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Trait>> GetPageOfTraits(PageInformationSender page)
        {
            return await _context.Traits.Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize).ToListAsync();
        }
    }
}
