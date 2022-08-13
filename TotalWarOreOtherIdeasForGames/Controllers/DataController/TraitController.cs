using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;
using TotalWarOreOtherIdeasForGames.DataBaseOperations;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class TraitController : Controller
    {
        private readonly TraitsOperations operations;

        public TraitController(TotalWarWanaBeContext context)
        {
            this.operations = new TraitsOperations(context);
        }

        // GET: Trait
        public async Task<IActionResult> IndexTrait(PageInformationSender page)
        {
            return View(await operations.GetPageOfTraits(page));
        }

        // GET: Trait/Details/5
        public async Task<IActionResult> DetailsTrait(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trait = await operations._context.Traits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trait == null)
            {
                return NotFound();
            }

            return View(trait);
        }

        // GET: Trait/Create
        public IActionResult CreateTrait()
        {
            ViewData["Formations_"] = new SelectList(operations._context.Formations.ToList(), "Id", "FormationName");
            return View();
        }

        // POST: Trait/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrait(TraitViewModel trait)
        {
            if (ModelState.IsValid)
            {
                operations._context.Traits.Add(trait.Trait_);
                foreach(int i in trait.Formations_){
                    operations._context.FormationTraits.Add(new FormationTrait(operations._context.Formations.FirstOrDefault(f => f.Id == i),trait.Trait_));
                }
                await operations._context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trait);
        }

        // GET: Trait/Edit/5
        public async Task<IActionResult> EditTrait(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TraitViewModel tvm = new TraitViewModel();
            tvm.Trait_ = await operations._context.Traits.FindAsync(id);
            if (tvm.Trait_ == null)
            {
                return NotFound();
            }
            var listFormation = operations._context.FormationTraits.Where(f => f.IdRight == id).ToList();
            // this needs to be removed
            tvm.Formations_ = new int[listFormation.Count];
            int i = 0;
            foreach(var forma in listFormation){
                tvm.Formations_[i] = forma.IdLeft;
                i++;
            }
            ViewData["Formations"] = new SelectList(operations._context.Formations, "Id", "FormationName");
            return View(tvm);
        }

        // POST: Trait/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTrait(int id, TraitViewModel traitViewModel)
        {
            if (id != traitViewModel.Trait_.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    operations._context.Traits.Update(traitViewModel.Trait_);
                    var oldListFactionFormation = operations._context.FormationTraits.Where(ft => ft.IdRight == traitViewModel.Trait_.Id).ToList();
                    foreach (int newId in traitViewModel.Formations_)
                    {
                        bool isNew = true;

                        foreach (FormationTrait ft in oldListFactionFormation)
                        {
                            if (ft.IdLeft == newId)
                            {
                                isNew = false;
                                break;
                            }
                        }
                        if (isNew)
                        {
                            /*After loading from the database in view and lodin again in oldList we search the database again*/
                            operations._context.FormationTraits.Add(new FormationTrait(operations._context.Formations.FirstOrDefault(form => form.Id == newId), traitViewModel.Trait_));
                        }
                    }

                    foreach (FormationTrait ft in oldListFactionFormation)
                    {
                        bool needRemove = true;
                        foreach (int newId in traitViewModel.Formations_)
                        {
                            if (ft.IdLeft == newId)
                            {
                                needRemove = false;
                            }
                        }
                        if (needRemove)
                        {
                            operations._context.FormationTraits.Remove(ft);
                        }
                    }
                    await operations._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TraitExists(traitViewModel.Trait_.Id))
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
            return View(traitViewModel);
        }
        public async Task<IActionResult> DeleteTrait(int id)
        {
            var trait = await operations._context.Traits.FindAsync(id);
            operations._context.Traits.Remove(trait);
            await operations._context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TraitExists(int id)
        {
            return operations._context.Traits.Any(e => e.Id == id);
        }
    }
}
