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
    public class HorseController : Controller
    {
        private readonly TotalWarWanaBeContext _context;

        public HorseController(TotalWarWanaBeContext context)
        {
            _context = context;
        }

        // GET: Horse
        public async Task<IActionResult> Index()
        {
            var totalWarWanaBeContext = _context.Horses.Include(h => h.BardingIdBardingNavigation);
            return View(await totalWarWanaBeContext.ToListAsync());
        }

        // GET: Horse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horse = await _context.Horses
                .Include(h => h.BardingIdBardingNavigation)
                .FirstOrDefaultAsync(m => m.IdHorse == id);
            if (horse == null)
            {
                return NotFound();
            }

            return View(horse);
        }

        // GET: Horse/Create
        public IActionResult Create()
        {
            ViewData["BardingIdBarding"] = new SelectList(_context.Bardings, "IdBarding", "IdBarding");
            return View();
        }

        // POST: Horse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHorse,AttackModifier,BreedName,DefenceModifiered,HorseStamina,HorseStrength,BardingIdBarding")] Horse horse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BardingIdBarding"] = new SelectList(_context.Bardings, "IdBarding", "IdBarding", horse.BardingIdBarding);
            return View(horse);
        }

        // GET: Horse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horse = await _context.Horses.FindAsync(id);
            if (horse == null)
            {
                return NotFound();
            }
            ViewData["BardingIdBarding"] = new SelectList(_context.Bardings, "IdBarding", "IdBarding", horse.BardingIdBarding);
            return View(horse);
        }

        // POST: Horse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHorse,AttackModifier,BreedName,DefenceModifiered,HorseStamina,HorseStrength,BardingIdBarding")] Horse horse)
        {
            if (id != horse.IdHorse)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorseExists(horse.IdHorse))
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
            ViewData["BardingIdBarding"] = new SelectList(_context.Bardings, "IdBarding", "IdBarding", horse.BardingIdBarding);
            return View(horse);
        }

        // GET: Horse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horse = await _context.Horses
                .Include(h => h.BardingIdBardingNavigation)
                .FirstOrDefaultAsync(m => m.IdHorse == id);
            if (horse == null)
            {
                return NotFound();
            }

            return View(horse);
        }

        // POST: Horse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horse = await _context.Horses.FindAsync(id);
            _context.Horses.Remove(horse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorseExists(int id)
        {
            return _context.Horses.Any(e => e.IdHorse == id);
        }
    }
}
