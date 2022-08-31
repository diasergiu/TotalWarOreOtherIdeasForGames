using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;
using TotalWarOreOtherIdeasForGames.DataBaseOperations;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    [Authorize(Roles = "Manager,Admin")]
    public class FormationController : Controller
    {
        private readonly ILogger logger;
        private readonly FormationsOperations formationOperations;

        public FormationController(TotalWarWanaBeContext context,ILogger<FormationController> logger)
        {
            this.logger = logger;
            this.formationOperations = new FormationsOperations(context);
        }

        // GET: Formation
        public async  Task<IActionResult> IndexFormation(int? CurrentPage, int? PageSize)
        {
            logger.LogInformation("[Formation]");
            if (CurrentPage == null || CurrentPage == 0)
            {
                CurrentPage = 1;
            }
            if (PageSize == null || PageSize == 0)
            {
                PageSize = 5;
            }
            return View(PageModel<Formation>.ToPageModel(formationOperations._context.Formations, (int)CurrentPage, (int)PageSize));
        }

        // GET: Formation/Details/5
        public async Task<IActionResult> DetailsFormation(int? id)
        {
            logger.LogInformation("[Formation]");
            if (id == null)
            {
                logger.LogCritical("[Formation]");
                return NotFound();
            }

            var Formation = await formationOperations.GetFormation(id);
            if (Formation == null)
            {
                return NotFound();
            }

            return View(Formation);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateFormation()
        {
            logger.LogInformation("[Formation]");
            ViewData["Horses"] = new SelectList(formationOperations._context.Horses, "Id", "BreedName");
            ViewData["Soldiers"] = new SelectList(formationOperations._context.SoldierModels, "Id", "SoldierName");
            ViewData["Traits"] = new SelectList(formationOperations._context.Traits, "Id", "TraitName");
            ViewData["Items"] = new SelectList(formationOperations._context.Items, "Id", "ItemName"); ;
            ViewData["Factions"] = new SelectList(formationOperations._context.Factions, "Id", "FactionName");
            return View();
        }

        // POST: Formation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFormation( FormationViewModel formation)
        {
            logger.LogInformation("[Formation]");
            if (ModelState.IsValid)
            {
                
                await formationOperations.SaveFormations(formation);
                return RedirectToAction(nameof(IndexFormation));
            }
            ViewData["horses"] = new SelectList(formationOperations._context.Horses, "Id", "Id", formation.Formation_.IdHorse);
            ViewData["Soldiers"] = new SelectList(formationOperations._context.SoldierModels, "Id", "Id", formation.Formation_.IdSoldier);
            ViewData["Traits"] = new SelectList(formationOperations._context.Traits, "Id", "TraitName");
            ViewData["Items"] = new SelectList(formationOperations._context.Items, "Id", "ItemName"); ;
            ViewData["Factions"] = new SelectList(formationOperations._context.Factions, "Id", "FactionName");
            return View(formation);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditFormation(int? id)
        {
            logger.LogInformation("[Formation]");
            if (id == null)
            {
                logger.LogCritical("[Formation]");
                return NotFound();
            }
            FormationViewModel formationViewModel = await formationOperations.GetViewModel((int)id);
            if (formationViewModel.Formation_ == null)
            {
                return NotFound();
            }
            ViewData["horses"] = new SelectList(formationOperations._context.Horses, "Id", "Id", formationViewModel.Formation_.IdHorse);
            ViewData["Soldiers"] = new SelectList(formationOperations._context.SoldierModels, "Id", "Id", formationViewModel.Formation_.IdSoldier);
            ViewData["Traits"] = new SelectList(formationOperations._context.Traits, "Id", "TraitName");
            ViewData["Items"] = new SelectList(formationOperations._context.Items, "Id", "ItemName"); ;
            ViewData["Factions"] = new SelectList(formationOperations._context.Factions, "Id", "FactionName");
            return View(formationViewModel);
        }

        // POST: Formation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFormation(int id,  FormationViewModel formationViewModel)
        {
            logger.LogInformation("[Formation]");
            if (id != formationViewModel.Formation_.Id)
            {
                logger.LogCritical("[Formation]");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await formationOperations.UpdateFormation(formationViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationExists(formationViewModel.Formation_.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Horses"] = new SelectList(formationOperations._context.Horses, "Id", "Id", formationViewModel.Formation_.IdHorse);
            ViewData["Soldiers"] = new SelectList(formationOperations._context.SoldierModels, "Id", "Id", formationViewModel.Formation_.IdSoldier);
            return View(formationViewModel);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFormation(int? id)
        {
            logger.LogInformation("[Formation]:");
            if (id == null)
            {
                logger.LogCritical("[Formation]:");
                return NotFound();
            }
            await formationOperations.DeleteFormation((int)id);
            return RedirectToAction(nameof(IndexFormation));
        }

        private bool FormationExists(int id)
        {
            return formationOperations._context.Formations.Any(e => e.Id == id);
        }
    }
}
