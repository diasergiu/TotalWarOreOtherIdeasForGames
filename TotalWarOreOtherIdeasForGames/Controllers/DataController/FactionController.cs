using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class FactionController : Controller
    {

        private readonly TotalWarWanaBeContext _context;

        public FactionController()
        {
            _context = new TotalWarWanaBeContext();
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Factions.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FactionName,FactionDescription")]Faction faction)
        {
            _context.Factions.Add(faction);
            await _context.SaveChangesAsync();
            // look if this works 
            return View("Create");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            return View(await _context.Factions.FirstOrDefaultAsync(f => f.IdFaction == id));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            return View(await _context.Factions.FirstOrDefaultAsync(f => f.IdFaction == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("FactionName,FactionDescription")]Faction faction)
        {
            if (faction.IdFaction != id) {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Factions.Update(faction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return RedirectToAction("Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
                {
                    return NotFound();
                }
            _context.Factions.Remove(await _context.Factions.FirstOrDefaultAsync(f => f.IdFaction == id));
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
