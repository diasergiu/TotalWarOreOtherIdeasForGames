using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations
{
    public class SoldierModelsOperations
    {
        public TotalWarWanaBeContext _context;

        public SoldierModelsOperations(TotalWarWanaBeContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<SoldierModel>> GetPageOfSoldiers(PageInformationSender page)
        {
            return await _context.SoldierModels.Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize).ToListAsync();
        }
    }
}
