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
    public class SoldierFormationController : Controller
    {
        private readonly TotalWarWanaBeContext _context;

        public SoldierFormationController(TotalWarWanaBeContext context)
        {
            _context = context;
        }

        // GET: SoldierFormation
        public async Task<IActionResult> Index()
        {
            var totalWarWanaBeContext = _context.SoldierFormations.Include(s => s.HorseIdHorseNavigation).Include(s => s.SoldierModelIdSoldierNavigation);
            return View(await totalWarWanaBeContext.ToListAsync());
        }

        // GET: SoldierFormation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldierFormation = await _context.SoldierFormations
                .Include(s => s.HorseIdHorseNavigation)
                .Include(s => s.SoldierModelIdSoldierNavigation)
                .FirstOrDefaultAsync(m => m.IdFormation == id);
            if (soldierFormation == null)
            {
                return NotFound();
            }

            return View(soldierFormation);
        }

        // GET: SoldierFormation/Create
        public IActionResult Create()
        {
            ViewData["HorseIdHorse"] = new SelectList(_context.Horses, "IdHorse", "IdHorse");
            ViewData["SoldierModelIdSoldier"] = new SelectList(_context.SoldierModels, "IdSoldier", "IdSoldier");
            return View();
        }

        // POST: SoldierFormation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormation,NumberSoldiers,StartingFormationValue,FormationName,SoldierModelIdSoldier,HorseIdHorse")] SoldierFormation soldierFormation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soldierFormation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HorseIdHorse"] = new SelectList(_context.Horses, "IdHorse", "IdHorse", soldierFormation.HorseIdHorse);
            ViewData["SoldierModelIdSoldier"] = new SelectList(_context.SoldierModels, "IdSoldier", "IdSoldier", soldierFormation.SoldierModelIdSoldier);
            return View(soldierFormation);
        }

        // GET: SoldierFormation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldierFormation = await _context.SoldierFormations.FindAsync(id);
            if (soldierFormation == null)
            {
                return NotFound();
            }
            ViewData["HorseIdHorse"] = new SelectList(_context.Horses, "IdHorse", "IdHorse", soldierFormation.HorseIdHorse);
            ViewData["SoldierModelIdSoldier"] = new SelectList(_context.SoldierModels, "IdSoldier", "IdSoldier", soldierFormation.SoldierModelIdSoldier);
            return View(soldierFormation);
        }

        // POST: SoldierFormation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFormation,NumberSoldiers,StartingFormationValue,FormationName,SoldierModelIdSoldier,HorseIdHorse")] SoldierFormation soldierFormation)
        {
            if (id != soldierFormation.IdFormation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soldierFormation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoldierFormationExists(soldierFormation.IdFormation))
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
            ViewData["HorseIdHorse"] = new SelectList(_context.Horses, "IdHorse", "IdHorse", soldierFormation.HorseIdHorse);
            ViewData["SoldierModelIdSoldier"] = new SelectList(_context.SoldierModels, "IdSoldier", "IdSoldier", soldierFormation.SoldierModelIdSoldier);
            return View(soldierFormation);
        }

        // GET: SoldierFormation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldierFormation = await _context.SoldierFormations
                .Include(s => s.HorseIdHorseNavigation)
                .Include(s => s.SoldierModelIdSoldierNavigation)
                .FirstOrDefaultAsync(m => m.IdFormation == id);
            if (soldierFormation == null)
            {
                return NotFound();
            }

            return View(soldierFormation);
        }

        // POST: SoldierFormation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soldierFormation = await _context.SoldierFormations.FindAsync(id);
            _context.SoldierFormations.Remove(soldierFormation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoldierFormationExists(int id)
        {
            return _context.SoldierFormations.Any(e => e.IdFormation == id);
        }
    }
}
