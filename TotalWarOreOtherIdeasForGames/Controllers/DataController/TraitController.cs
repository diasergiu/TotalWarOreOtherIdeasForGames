using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TotalWarDLA.Models;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class TraitController : Controller
    {
        private readonly TotalWarWanaBeContext _context;

        public TraitController(TotalWarWanaBeContext context)
        {
            _context = context;
        }

        // GET: Trait
        public async Task<IActionResult> IndexTrait()
        {
            return View(await _context.Traits.ToListAsync());
        }

        // GET: Trait/Details/5
        public async Task<IActionResult> DetailsTrait(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trait = await _context.Traits
                .FirstOrDefaultAsync(m => m.IdTrait == id);
            if (trait == null)
            {
                return NotFound();
            }

            return View(trait);
        }

        // GET: Trait/Create
        public IActionResult CreateTrait()
        {
            ViewData["Formations_"] = new SelectList(_context.Formations.ToList(), "IdFormation", "FormationName");
            return View();
        }

        // POST: Trait/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrait([Bind("IdTrait,TraitDescription,TraitName")] TraitViewModel trait)
        {
            if (ModelState.IsValid)
            {
                _context.Traits.Add(trait.Trait_);
                foreach(int i in trait.Formations_){
                    _context.FormationTraits.Add(new FormationTrait(_context.Formations.FirstOrDefault(f => f.IdFormation == i),trait.Trait_));
                }
                await _context.SaveChangesAsync();
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
            tvm.Trait_ = await _context.Traits.FindAsync(id);
            if (tvm.Trait_ == null)
            {
                return NotFound();
            }
            var listFormation = _context.FormationTraits.Where(f => f.IdTrait == id).ToList();
            // this needs to be removed
            tvm.Formations_ = new int[listFormation.Count];
            int i = 0;
            foreach(var forma in listFormation){
                tvm.Formations_[i] = forma.IdFormation;
                i++;
            }
            ViewData["IdFormation"] = new SelectList(_context.Formations, "IdFormation", "FormationName");
            return View(tvm);
        }

        // POST: Trait/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTrait(int id, TraitViewModel traitViewModel)
        {
            if (id != traitViewModel.Trait_.IdTrait)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Traits.Update(traitViewModel.Trait_);
                    var oldListFactionFormation = _context.FormationTraits.Where(ft => ft.IdTrait == traitViewModel.Trait_.IdTrait).ToList();
                    foreach (int newId in traitViewModel.Formations_)
                    {
                        bool isNew = true;

                        foreach (FormationTrait ft in oldListFactionFormation)
                        {
                            if (ft.IdFormation == newId)
                            {
                                isNew = false;
                                break;
                            }
                        }
                        if (isNew)
                        {
                            /*After loading from the database in view and lodin again in oldList we search the database again*/
                            _context.FormationTraits.Add(new FormationTrait(_context.Formations.FirstOrDefault(form => form.IdFormation == newId), traitViewModel.Trait_));
                        }
                    }

                    foreach (FormationTrait ft in oldListFactionFormation)
                    {
                        bool needRemove = true;
                        foreach (int newId in traitViewModel.Formations_)
                        {
                            if (ft.IdFormation == newId)
                            {
                                needRemove = false;
                            }
                        }
                        if (needRemove)
                        {
                            _context.FormationTraits.Remove(ft);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TraitExists(traitViewModel.Trait_.IdTrait))
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

        // GET: Trait/Delete/5
        public async Task<IActionResult> DeleteTrait(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trait = await _context.Traits
                .FirstOrDefaultAsync(m => m.IdTrait == id);
            if (trait == null)
            {
                return NotFound();
            }

            return View(trait);
        }

        // POST: Trait/Delete/5
        [HttpPost, ActionName("DeleteTrait")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trait = await _context.Traits.FindAsync(id);
            _context.Traits.Remove(trait);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TraitExists(int id)
        {
            return _context.Traits.Any(e => e.IdTrait == id);
        }
    }
}
