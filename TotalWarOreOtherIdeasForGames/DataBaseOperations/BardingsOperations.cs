using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations
{
    public class BardingsOperations
    {
        // looks better as to way they made it readonly
        public TotalWarWanaBeContext _context;

        public BardingsOperations(TotalWarWanaBeContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Barding>> GetPageOfBarding(PageInformationSender page)
        {
            return await _context.Bardings.Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize).ToListAsync();
        }
    }
}
