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
    public class FormationController : Controller
    {
        private readonly TotalWarWanaBeContext _context;

        public FormationController(TotalWarWanaBeContext context)
        {
            _context = context;
        }

        // GET: Formation
        public async Task<IActionResult> Index()
        {
            var totalWarWanaBeContext = _context.Formations.Include(s => s.IdHorseNavigation).Include(s => s.IdSoldierNavigation);
            return View(await totalWarWanaBeContext.ToListAsync());
        }

        // GET: Formation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Formation = await _context.Formations
                .Include(s => s.IdHorseNavigation)
                .Include(s => s.IdSoldierNavigation)
                .FirstOrDefaultAsync(m => m.IdFormation == id);
            if (Formation == null)
            {
                return NotFound();
            }

            return View(Formation);
        }

        // GET: Formation/Create
        public IActionResult Create()
        {
            ViewData["IdHorse"] = new SelectList(_context.Horses, "IdHorse", "IdHorse");
            ViewData["IdSoldier"] = new SelectList(_context.SoldierModels, "IdSoldier", "IdSoldier");
            return View();
        }

        // POST: Formation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormation,NumberSoldiers,StartingFormationValue,FormationName,IdSoldier,IdHorse")] Formation Formation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Formation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHorse"] = new SelectList(_context.Horses, "IdHorse", "IdHorse", Formation.IdHorse);
            ViewData["IdSoldier"] = new SelectList(_context.SoldierModels, "IdSoldier", "IdSoldier", Formation.IdSoldier);
            return View(Formation);
        }

        // GET: Formation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Formation = await _context.Formations.FindAsync(id);
            if (Formation == null)
            {
                return NotFound();
            }
            ViewData["IdHorse"] = new SelectList(_context.Horses, "IdHorse", "IdHorse", Formation.IdHorse);
            ViewData["IdSoldier"] = new SelectList(_context.SoldierModels, "IdSoldier", "IdSoldier", Formation.IdSoldier);
            return View(Formation);
        }

        // POST: Formation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFormation,NumberSoldiers,StartingFormationValue,FormationName,IdSoldier,IdHorse")] Formation Formation)
        {
            if (id != Formation.IdFormation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Formation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationExists(Formation.IdFormation))
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
            ViewData["IdHorse"] = new SelectList(_context.Horses, "IdHorse", "IdHorse", Formation.IdHorse);
            ViewData["IdSoldier"] = new SelectList(_context.SoldierModels, "IdSoldier", "IdSoldier", Formation.IdSoldier);
            return View(Formation);
        }

        // GET: Formation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Formation = await _context.Formations
                .Include(s => s.IdHorseNavigation)
                .Include(s => s.IdSoldierNavigation)
                .FirstOrDefaultAsync(m => m.IdFormation == id);
            if (Formation == null)
            {
                return NotFound();
            }

            return View(Formation);
        }

        // POST: Formation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Formation = await _context.Formations.FindAsync(id);
            _context.Formations.Remove(Formation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormationExists(int id)
        {
            return _context.Formations.Any(e => e.IdFormation == id);
        }
    }
}
