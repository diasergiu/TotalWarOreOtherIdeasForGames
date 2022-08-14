using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger logger;

        public FactionController(TotalWarWanaBeContext context, ILogger<FactionController> logger)
        {
            this.logger = logger;
            this.factionOperations = new FactionsOperations(context);
        }
        public async Task<IActionResult> IndexFaction(int? CurrentPage, int? PageSize)
        {
            logger.LogInformation("[Faction]: Oppened index with curent page {0} and page size {1} ", CurrentPage, PageSize);
            if (CurrentPage == null || CurrentPage == 0)
            {
                CurrentPage = 1;
            }
            if (PageSize == null || PageSize == 0)
            {
                PageSize = 5;
            }
            return View(PageModel<Faction>.ToPageModel(factionOperations._context.Factions, (int)CurrentPage, (int)PageSize));
        }
        public async Task<IActionResult> CreateFaction()
        {
            logger.LogInformation("[Faction]: Accesing the create View ");
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
            logger.LogInformation("[Faction]: you are creating the faction {0}", faction.Faction_.FactionName);
            await factionOperations.CreateFaction(faction);           
            return RedirectToAction("CreateFaction");
        }
        public async Task<IActionResult> DetailsFaction(int? id)
        {
            logger.LogInformation("[Faction]: Accesing the faction {0} details", id);
            if(id == null)
            {
                logger.LogCritical("[Faction]: Id was null");
                return NotFound();
            }
            return View(await factionOperations._context.Factions.FirstOrDefaultAsync(f => f.Id == id));

        }
        public async Task<IActionResult> EditFaction(int? id)
        {
            logger.LogInformation("[Faction]: Accesing the edit view for {0}", id);
            if(id == null)
            {
                logger.LogCritical("[Faction]: Id was null");
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
            logger.LogInformation("[Faction]: Editing the Faction with id {0}", id);
            if (factionViewModel.Faction_.Id != id) {
                logger.LogCritical("[Faction]: Id was null");
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
        public async Task<IActionResult> DeleteFaction(int? id)
        {
            logger.LogInformation("[Faction]: Attempting to delete the faction with id {0} ", id);
            if(id == null)
            {
                logger.LogCritical("[Faction]: Id was null");
                return NotFound();
            }
            factionOperations._context.Factions.Remove(await factionOperations._context.Factions.FirstOrDefaultAsync(f => f.Id == id));
            await factionOperations._context.SaveChangesAsync();
            return RedirectToAction("IndexFaction");
        }
    }
}
