using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TotalWarDLA.Models;
using TotalWarOreOtherIdeasForGames.DataBaseOperations;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class FormationController : Controller
    {
        private readonly FormationsOperation formationOperations;

        public FormationController(TotalWarWanaBeContext context)
        {
            this.formationOperations = new FormationsOperation(context);
        }

        // GET: Formation
        public IActionResult IndexFormation()
        {
            var totalWarWanaBeContext = formationOperations.GetAllFormations();
            return View(totalWarWanaBeContext);
        }

        // GET: Formation/Details/5
        public async Task<IActionResult> DetailsFormation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Formation = await formationOperations.GetFormation(id);
            if (Formation == null)
            {
                return NotFound();
            }

            return View(Formation);
        }

        // GET: Formation/Create
        public IActionResult CreateFormation()
        {
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

        // GET: Formation/Edit/5
        public async Task<IActionResult> EditFormation(int? id)
        {
            if (id == null)
            {
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
            if (id != formationViewModel.Formation_.Id)
            {
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

        // GET: Formation/Delete/5
        [ActionName("DeleteFormation")]
        public async Task<IActionResult> DeleteFormation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Formation = await formationOperations.GetFormation(id);
            if (Formation == null)
            {
                return NotFound();
            }

            return View(Formation);
        }

        // POST: Formation/Delete/5
        [HttpPost, ActionName("DeleteFormation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await formationOperations.DeleteFormation(id);
            return RedirectToAction(nameof(IndexFormation));
        }

        private bool FormationExists(int id)
        {
            return formationOperations._context.Formations.Any(e => e.Id == id);
        }
    }
}
