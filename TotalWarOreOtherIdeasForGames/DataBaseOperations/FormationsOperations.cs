using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.NonDataModels;
using TotalWarDLA.Models.Pagination;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations
{
    public class FormationsOperations
    {
        public readonly TotalWarWanaBeContext _context;
        // not in use yet 
        public FormationsOperations(TotalWarWanaBeContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Formation>> GetPageOfFormations(PageInformationSender page)
        {
            return await _context.Formations.Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize).ToListAsync();
        }

        #region save Formation
        public async Task SaveFormations(FormationViewModel formationVM){
            _context.Formations.Add(formationVM.Formation_);
            await SaveItem(formationVM.Items_, formationVM.Formation_);
            await SaveTrait(formationVM.Traits_, formationVM.Formation_);
            await SaveFactions(formationVM.Factions_, formationVM.Formation_);
            await _context.SaveChangesAsync();

        }
        private async Task SaveItem(int[] items, Formation formation){
            for(int i = 0; i < items.Length; i++){
                _context.ItemFormations.Add(new ItemFormation(await _context.Items.FirstOrDefaultAsync(q => q.Id == items[i]), formation));
            }
        }
        private async Task SaveTrait(int[] traits, Formation formation)
        {
            for (int i = 0; i < traits.Length; i++)
            {
                _context.FormationTraits.Add(new FormationTrait(formation,await _context.Traits.FirstOrDefaultAsync(q => q.Id == traits[i])));
            }
        }
        private async Task SaveFactions(int[] factions, Formation formation)
        {
            for (int i = 0; i < factions.Length; i++)
            {
                _context.FactionFormations.Add(new FactionFormation(await _context.Factions.FirstOrDefaultAsync(q => q.Id == factions[i]), formation));
            }
        }
        #endregion

        #region Update Formation
        public async Task UpdateFormation(FormationViewModel formationViewModel)
        {

            _context.Update(formationViewModel.Formation_);
            UpdateItemFormation(formationViewModel);
            UpdateTraitFormation(formationViewModel);
            updateFactionFormation(formationViewModel);
            await _context.SaveChangesAsync();
        }

        private void UpdateItemFormation(FormationViewModel formationViewModel) 
        {
            var oldListItemFormation = _context.ItemFormations.Where(ff => ff.IdFormation == formationViewModel.Formation_.Id).ToList();
            foreach (int newId in formationViewModel.Items_)
            {
                bool isNew = true;

                foreach (ItemFormation FF in oldListItemFormation)
                {
                    if (FF.IdItem == newId)
                    {
                        isNew = false;
                        break;
                    }
                }
                if (isNew)
                {
                    _context.ItemFormations.Add(new ItemFormation(_context.Items.FirstOrDefault(itm => itm.Id == newId), formationViewModel.Formation_));
                }
            }

            foreach (ItemFormation ff in oldListItemFormation)
            {
                bool needRemove = true;
                foreach (int newId in formationViewModel.Items_)
                {
                    if (ff.IdItem == newId)
                    {
                        needRemove = false;
                        break;
                    }
                }
                if (needRemove)
                {
                    _context.ItemFormations.Remove(ff);
                }
            }
        }

        private void UpdateTraitFormation(FormationViewModel formationViewModel)
        {
            var oldListTraitFormation = _context.FormationTraits.Where(ff => ff.IdLeft == formationViewModel.Formation_.Id).ToList();
            foreach (int newId in formationViewModel.Traits_)
            {
                bool isNew = true;

                foreach (FormationTrait FF in oldListTraitFormation)
                {
                    if (FF.IdRight == newId)
                    {
                        isNew = false;
                        break;
                    }
                }
                if (isNew)
                {
                    _context.FormationTraits.Add(new FormationTrait(formationViewModel.Formation_, _context.Traits.FirstOrDefault(itm => itm.Id == newId)));
                }
            }

            foreach (FormationTrait ff in oldListTraitFormation)
            {
                bool needRemove = true;
                foreach (int newId in formationViewModel.Traits_)
                {
                    if (ff.IdRight == newId)
                    {
                        needRemove = false;
                        break;
                    }
                }
                if (needRemove)
                {
                    _context.FormationTraits.Remove(ff);
                }
            }
        }

        private void updateFactionFormation(FormationViewModel formationViewModel)
        {
            var oldListFactionFormation = _context.FactionFormations.Where(ff => ff.IdFormation == formationViewModel.Formation_.Id).ToList();
            foreach (int newId in formationViewModel.Factions_)
            {
                bool isNew = true;

                foreach (FactionFormation FF in oldListFactionFormation)
                {
                    if (FF.IdFaction == newId)
                    {
                        isNew = false;
                        break;
                    }
                }
                if (isNew)
                {
                    _context.FactionFormations.Add(new FactionFormation(_context.Factions.FirstOrDefault(itm => itm.Id == newId), formationViewModel.Formation_));
                }
            }

            foreach (FactionFormation ff in oldListFactionFormation)
            {
                bool needRemove = true;
                foreach (int newId in formationViewModel.Factions_)
                {
                    if (ff.IdFaction == newId)
                    {
                        needRemove = false;
                        break;
                    }
                }
                if (needRemove)
                {
                    _context.FactionFormations.Remove(ff);
                }
            }
        }
             

        #endregion

        public async Task DeleteFormation(int id)
        {
            var Formation = await _context.Formations.FindAsync(id);
            _context.Formations.Remove(Formation);
            await _context.SaveChangesAsync();
        }

        public async Task<Formation> GetFormation(int? id)
        {
            return await _context.Formations
                .Include(s => s.IdHorseNavigation)
                .Include(s => s.IdSoldierNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public IList<Formation> GetAllFormations()
        {
            return _context.Formations.Include(s => s.IdHorseNavigation).Include(s => s.IdSoldierNavigation).ToList();
        }

        public async  Task<FormationViewModel> GetViewModel(int id)
        {
            FormationViewModel formationViewModel = new FormationViewModel();
            formationViewModel.Formation_ = await _context.Formations.FindAsync(id);
            var listItems = _context.ItemFormations.Where(i => i.IdFormation == id);
            int i = 0;
            formationViewModel.Items_ = new int[listItems.Count()];
            // split this into multiple functions later
            foreach (var commonItem in listItems)
            {
                formationViewModel.Items_[i] = commonItem.IdItem;
                i++;
            }
            var listTraits = _context.FormationTraits.Where(i => i.IdLeft == id);
            i = 0;
            formationViewModel.Traits_ = new int[listTraits.Count()];
            foreach (var commonTrait in listTraits)
            {
                formationViewModel.Traits_[i] = commonTrait.IdRight;
                i++;
            }
            var listFactions = _context.FactionFormations.Where(i => i.IdFormation == id);
            i = 0;
            formationViewModel.Factions_ = new int[listFactions.Count()];
            foreach (var commonFaction in listFactions)
            {
                formationViewModel.Factions_[i] = commonFaction.IdFaction;
                i++;
            }
            return formationViewModel;
        }
    }
}
