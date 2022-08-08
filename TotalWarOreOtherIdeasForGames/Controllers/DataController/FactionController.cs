using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;
using TotalWarOreOtherIdeasForGames.DataBaseOperations;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class FactionController : Controller
    {

        private readonly FactionsOperations factionOperations;

        public FactionController(TotalWarWanaBeContext context)
        {
            this.factionOperations = new FactionsOperations(context);
        }
        public async Task<IActionResult> IndexFaction(PageInformationSender page)
        {
            return View(await factionOperations.GetPageOfFactions(page));
        }
        public async Task<IActionResult> CreateFaction()
        {
            // nu imi place cum arata parte asta si ar trebui sa ma uit la ce este view models mai mult simt ca nu invat nimic aici doar aplic ce stiu deja
            FactionViewModel factionViewModel = new FactionViewModel();
            factionViewModel.ListFormations = await factionOperations._context.Formations.ToListAsync();
            //look at way is this not working
            ViewData["Formations"] = new SelectList(factionOperations._context.Formations, "Id", "FormationName");
            return View(factionViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFaction( FactionViewModel faction)
        {
            await factionOperations.CreateFaction(faction);           
            return RedirectToAction("CreateFaction");
        }
        public async Task<IActionResult> DetailsFaction(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            return View(await factionOperations._context.Factions.FirstOrDefaultAsync(f => f.Id == id));

        }
        public async Task<IActionResult> EditFaction(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            // pe bune
            FactionViewModel FVM = await factionOperations.getViewModel((int)id);
            return View(FVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFaction(int id,/*[Bind("Id,FactionName,FactionDescription,Formations_")]*/FactionViewModel factionViewModel)
        {
            if (factionViewModel.Faction_.Id != id) {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // not a good alghorithm for dooing this first i should send the list of formations not just the ID's
                // second i should probabli itherate throw 1'ts not tuice 
                // third i should if i could delete elements that are in common from the list while itherating throw them 
                await factionOperations.UpdateFaction(factionViewModel);
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
            factionOperations._context.Factions.Remove(await factionOperations._context.Factions.FirstOrDefaultAsync(f => f.Id == id));
            await factionOperations._context.SaveChangesAsync();
            return RedirectToAction("IndexFaction");
        }
    }
}
