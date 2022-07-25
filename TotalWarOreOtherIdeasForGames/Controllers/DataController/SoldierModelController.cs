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
    public class SoldierModelController : Controller
    {
        private readonly TotalWarWanaBeContext _context;

        public SoldierModelController(TotalWarWanaBeContext context)
        {
            _context = context;
        }

        // GET: SoldierModels
        public async Task<IActionResult> IndexSoldierModel()
        {
            return View(await _context.SoldierModels.ToListAsync());
        }

        // GET: SoldierModels/Details/5
        public async Task<IActionResult> DetailsSoldierModel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldierModel = await _context.SoldierModels
                .FirstOrDefaultAsync(m => m.IdSoldier == id);
            if (soldierModel == null)
            {
                return NotFound();
            }

            return View(soldierModel);
        }

        // GET: SoldierModels/Create
        public IActionResult CreateSoldierModel()
        {
            return View();
        }

        // POST: SoldierModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSoldierModel([Bind("IdSoldier,AttackSkilll,DefenceSkill,Stamina,Speed,Acuracy,SoldierName")] SoldierModel soldierModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soldierModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soldierModel);
        }

        // GET: SoldierModels/Edit/5
        public async Task<IActionResult> EditSoldierModel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldierModel = await _context.SoldierModels.FindAsync(id);
            if (soldierModel == null)
            {
                return NotFound();
            }
            return View(soldierModel);
        }

        // POST: SoldierModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSoldierModel(int id, [Bind("IdSoldier,AttackSkilll,DefenceSkill,Stamina,Speed,Acuracy,SoldierName")] SoldierModel soldierModel)
        {
            if (id != soldierModel.IdSoldier)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soldierModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoldierModelExists(soldierModel.IdSoldier))
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
            return View(soldierModel);
        }

        // GET: SoldierModels/Delete/5
        public async Task<IActionResult> DeleteSoldierModel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldierModel = await _context.SoldierModels
                .FirstOrDefaultAsync(m => m.IdSoldier == id);
            if (soldierModel == null)
            {
                return NotFound();
            }

            return View(soldierModel);
        }

        // POST: SoldierModels/Delete/5
        [HttpPost, ActionName("DeleteSoldierModel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soldierModel = await _context.SoldierModels.FindAsync(id);
            _context.SoldierModels.Remove(soldierModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoldierModelExists(int id)
        {
            return _context.SoldierModels.Any(e => e.IdSoldier == id);
        }
    }
}
