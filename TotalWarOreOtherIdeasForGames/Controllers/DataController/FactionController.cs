using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class FactionController : Controller
    {

        private readonly TotalWarWanaBeContext _context;

        public FactionController()
        {
            _context = new TotalWarWanaBeContext();
        }
        public async Task<IActionResult> IndexFaction()
        {
            return View(await _context.Factions.ToListAsync());
        }
        public async Task<IActionResult> CreateFaction()
        {
            // nu imi place cum arata parte asta si ar trebui sa ma uit la ce este view models mai mult simt ca nu invat nimic aici doar aplic ce stiu deja
            FactionViewModel factionViewModel = new FactionViewModel();
            factionViewModel.ListFormations = await _context.Formations.ToListAsync();
            //look at way is this not working
            ViewData["IdFormation"] = new SelectList(_context.Formations, "IdFormation", "FormationName");
            return View(factionViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFaction( FactionViewModel faction)
        {
            _context.Factions.Add(faction.Faction_);
            foreach(int idFormation in faction.Formations_)
            {
                _context.FactionFormations.Add(new FactionFormation(faction.Faction_,await _context.Formations.FirstOrDefaultAsync(format => format.IdFormation == idFormation)));
            }
            await _context.SaveChangesAsync();
           
            return RedirectToAction("CreateFaction");
        }
        public async Task<IActionResult> DetailsFaction(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            return View(await _context.Factions.FirstOrDefaultAsync(f => f.IdFaction == id));

        }
        public async Task<IActionResult> EditFaction(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            // pe bune
            FactionViewModel FVM = new FactionViewModel();
            FVM.Faction_ = await _context.Factions.Include("FactionFormations").FirstOrDefaultAsync(f => f.IdFaction == id);
            FVM.ListFormations = _context.Formations;
            FVM.Formations_ =new int[FVM.Faction_.FactionFormations.Count];
            int i = 0;
            foreach(var idFormation in FVM.Faction_.FactionFormations)
            {
                FVM.Formations_[i] = idFormation.IdFormation;
                i++;
            }
            return View(FVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFaction(int id,/*[Bind("IdFaction,FactionName,FactionDescription,Formations_")]*/FactionViewModel factionViewModel)
        {
            if (factionViewModel.Faction_.IdFaction != id) {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // not a good alghorithm for dooing this first i should send the list of formations not just the ID's
                // second i should probabli itherate throw 1'ts not tuice 
                // third i should if i could delete elements that are in common from the list while itherating throw them 
                var oldListFactionFormation = _context.FactionFormations.Where(ff => ff.IdFaction == factionViewModel.Faction_.IdFaction).ToList();
                foreach(int newId in factionViewModel.Formations_)
                {
                    bool isNew = true;

                    foreach (FactionFormation FF in oldListFactionFormation){
                        if (FF.IdFormation == newId){
                            isNew = false;
                            break;
                        }
                    }
                    if (isNew)
                    {
                        _context.FactionFormations.Add(new FactionFormation(factionViewModel.Faction_, _context.Formations.FirstOrDefault(form => form.IdFormation == newId/*After loading from the database in view and lodin again in oldList we search the database again*/)));
                    }
                }

                foreach(FactionFormation ff in oldListFactionFormation)
                {
                    bool needRemove = true;
                    foreach(int newId in factionViewModel.Formations_)
                    {
                        if(ff.IdFormation == newId)
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
            return RedirectToAction("EditFaction");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFaction(int? id)
        {
            if(id == null)
                {
                    return NotFound();
                }
            _context.Factions.Remove(await _context.Factions.FirstOrDefaultAsync(f => f.IdFaction == id));
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexFaction");
        }
    }
}
