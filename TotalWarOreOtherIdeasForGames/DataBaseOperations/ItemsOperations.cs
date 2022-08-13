using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations
{
    public class ItemsOperations
    {
        public TotalWarWanaBeContext _context;

        public ItemsOperations(TotalWarWanaBeContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Item>> GetPageOfItems(PageModel<Item> page)
        {
            return await _context.Items.Skip((page.CurrentPage - 1) * page.PageSize)
                .Take(page.PageSize).ToListAsync();
        }
    }
}
