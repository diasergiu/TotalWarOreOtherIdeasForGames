using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations
{
    public class FormationsOperation
    {
        private readonly TotalWarWanaBeContext _context;
        // not in use yet 
        public FormationsOperation(TotalWarWanaBeContext context)
        {
            this._context = context;
        }

        public async void SaveFormations(FormationViewModel formationVM){
            _context.Formations.Add(formationVM.Formation_);
            //await SaveItem(formationVM.Items_, formationVM.Formation_);
            //await SaveTrait(formationVM.Traits_, formationVM.Formation_);
            //await SaveFactions(formationVM.Factions_, formationVM.Formation_);
            for (int i = 0; i < formationVM.Traits_.Length; i++)
            {
                _context.FormationTraits.Add(new FormationTrait(formationVM.Formation_, await _context.Traits.FirstOrDefaultAsync(q => q.IdTrait == formationVM.Traits_[i])));
            }
            for (int i = 0; i < formationVM.Factions_.Length; i++)
            {
                _context.FactionFormations.Add(new FactionFormation(await _context.Factions.FirstOrDefaultAsync(q => q.IdFaction == formationVM.Factions_[i]), formationVM.Formation_));
            }
            for (int i = 0; i < formationVM.Items_.Length; i++)
            {
                _context.ItemFormations.Add(new ItemFormation(await _context.Items.FirstOrDefaultAsync(q => q.IdItem == formationVM.Items_[i]), formationVM.Formation_));
            }
            await _context.SaveChangesAsync();

        } 
        private async void SaveItem(int[] items, Formation formation){
            for(int i = 0; i < items.Length; i++){
                _context.ItemFormations.Add(new ItemFormation(await _context.Items.FirstOrDefaultAsync(q => q.IdItem == i), formation));
            }
        }
        private async void SaveTrait(int[] traits, Formation formation)
        {
            for (int i = 0; i < traits.Length; i++)
            {
                _context.FormationTraits.Add(new FormationTrait(formation,await _context.Traits.FirstOrDefaultAsync(q => q.IdTrait == i)));
            }
        }
        private async void SaveFactions(int[] factions, Formation formation)
        {
            for (int i = 0; i < factions.Length; i++)
            {
                _context.FactionFormations.Add(new FactionFormation(await _context.Factions.FirstOrDefaultAsync(q => q.IdFaction == i), formation));
            }
        }


    }
}
