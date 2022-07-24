using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TotalWarDLA.Models;

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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Traits.ToListAsync());
        }

        // GET: Trait/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trait/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTrait,TraitDescription,TraitName")] Trait trait)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trait);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trait);
        }

        // GET: Trait/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trait = await _context.Traits.FindAsync(id);
            if (trait == null)
            {
                return NotFound();
            }
            return View(trait);
        }

        // POST: Trait/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTrait,TraitDescription,TraitName")] Trait trait)
        {
            if (id != trait.IdTrait)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trait);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TraitExists(trait.IdTrait))
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
            return View(trait);
        }

        // GET: Trait/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
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
