using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations
{
    public class FactionsOperations
    {
        public TotalWarWanaBeContext _context;

        public FactionsOperations(TotalWarWanaBeContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Faction>> GetPageOfFactions(PageInformationSender page)
        {
            return await _context.Factions.Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize).ToListAsync();
        }

        public async Task<FactionViewModel> getViewModel(int id)
        {
            FactionViewModel FVM = new FactionViewModel();
            FVM.Faction_ = await _context.Factions.Include("FactionFormations").FirstOrDefaultAsync(f => f.Id == id);
            FVM.ListFormations = _context.Formations;
            FVM.Formations_ = new int[FVM.Faction_.FactionFormations.Count];
            int i = 0;
            foreach (var idFormation in FVM.Faction_.FactionFormations)
            {
                FVM.Formations_[i] = idFormation.IdFormation;
                i++;
            }

            return FVM;
        }

        public async Task UpdateFaction(FactionViewModel factionViewModel)
        {
            var oldListFactionFormation = _context.FactionFormations.Where(ff => ff.IdFaction == factionViewModel.Faction_.Id).ToList();
            foreach (int newId in factionViewModel.Formations_)
            {
                bool isNew = true;

                foreach (FactionFormation FF in oldListFactionFormation)
                {
                    if (FF.IdFormation == newId)
                    {
                        isNew = false;
                        break;
                    }
                }
                if (isNew)
                {
                    _context.FactionFormations.Add(new FactionFormation(factionViewModel.Faction_, _context.Formations.FirstOrDefault(form => form.Id == newId/*After loading from the database in view and lodin again in oldList we search the database again*/)));
                }
            }

            foreach (FactionFormation ff in oldListFactionFormation)
            {
                bool needRemove = true;
                foreach (int newId in factionViewModel.Formations_)
                {
                    if (ff.IdFormation == newId)
                    {
                        needRemove = false;
                    }
                }
                if (needRemove)
                {
                    _context.FactionFormations.Remove(ff);
                }
            }
            try
            {
                _context.Factions.Update(factionViewModel.Faction_);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task CreateFaction(FactionViewModel faction)
        {
            _context.Factions.Add(faction.Faction_);
            foreach (int idFormation in faction.Formations_)
            {
                _context.FactionFormations.Add(new FactionFormation(faction.Faction_, await _context.Formations.FirstOrDefaultAsync(format => format.Id == idFormation)));
            }
            await _context.SaveChangesAsync();
        }
    }
}
